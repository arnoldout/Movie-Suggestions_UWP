using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<Movie> results = new List<Movie>();
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            ddlName.Items.Clear();
            ddlName.Visibility = Visibility.Collapsed;
            pBarMainPage.IsActive = true;
            string uri = "https://matchingsocks.herokuapp.com/search/movie/" + tbName.Text;
                WebRequest wrGETURL = WebRequest.Create(uri);
            wrGETURL.Proxy = null;
            try
            {
                WebResponse response = await wrGETURL.GetResponseAsync();
                Stream dataStream = response.GetResponseStream();
                StreamReader objReader = new StreamReader(dataStream);
            
                dynamic movie = JsonConvert.DeserializeObject(objReader.ReadToEnd());

                results = new List<Movie>();
                
                StringBuilder sb = new StringBuilder();
                foreach (dynamic results in movie)
                {
                    this.results.Add(new Movie((int)results.id, System.Convert.ToString(results.poster_path), System.Convert.ToString(results.title), System.Convert.ToString(results.overview), System.Convert.ToString(results.release_date), System.Convert.ToString(results.vote_average), System.Convert.ToString(results.backdrop_path)));
                    sb.Append(Convert.ToString(results.title) + "\n");
                }
                
                ddlName.Visibility = Visibility.Visible;
                if(results.Count > 0)
                    foreach (Movie m in results)
                    {
                        //display the name and the release year in parenthesis
                        ddlName.Items.Add(m.Title + " (" + m.ReleaseDate.Substring(0, 4) + ")");
                        ddlName.PlaceholderText = "Results";
                    }
                else
                {
                    ddlName.PlaceholderText = "No Movies Found";
                    ddlName.Items.Add("No Movies Found");
                }

                response.Dispose();
            }
            catch (Exception ex)
            {
                String s = ex.Message;
                s = s;
            }
            pBarMainPage.IsActive = false;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void ddlName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            goToResults(results[combo.SelectedIndex].Id.ToString());
        }

        private void btnID_Click(object sender, RoutedEventArgs e)
        {
            goToResults(tbID.Text);
        }
        public async void goToResults(String id)
        {
            try
            {
                App.searchID = Convert.ToInt32(id);
                this.Frame.Navigate(typeof(Results));
            }
            catch (Exception ex)
            {
                var c = new ContentDialog()
                {
                    Title = ex.Message,
                    PrimaryButtonText = "OK"
                };
                await c.ShowAsync();
            }
        }

        private void tbName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            String s = tb.Text;
            if (tb.Text.Equals("Name: "))
            {
                tb.Text = "";
            }
        }

        private void tbID_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            String s = tb.Text;
            if (tb.Text.Equals("ID: "))
            {
                tb.Text = "";
            }
        }
    }
}
