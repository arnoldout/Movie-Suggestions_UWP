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
        //Global list for movie results. Will be used to populate the combobox.
        List<Movie> results = new List<Movie>();
        public MainPage()
        {
            this.InitializeComponent();
            //if redirected here from results page, error  message may have a vaue
            //display the message to screen, reset value
            if(!App.errorMsg.Equals(""))
            {
                errorMessage.Visibility = Visibility.Visible;
                errorMessage.Text = App.errorMsg;
                App.errorMsg = "";
            }
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            /*
                When the search button is clicked, a get request is sent to the heroku server asking it to search theMovieDB for similarly named movies.
                The Heroku server will refine the results and return json here. 
                That json is read into movie objects, and the names are displayed in a combobox.
            */
            errorMessage.Visibility = Visibility.Collapsed;
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
                
                //StringBuilder sb = new StringBuilder();
                foreach (dynamic results in movie)
                {
                    this.results.Add(new Movie((int)results.id, System.Convert.ToString(results.poster_path), System.Convert.ToString(results.title), System.Convert.ToString(results.overview), System.Convert.ToString(results.release_date), System.Convert.ToString(results.vote_average), System.Convert.ToString(results.backdrop_path)));
                }
                
                
                //if there are results, display them in the combobox
                
                if(results.Count > 0)
                { 
                    ddlName.Visibility = Visibility.Visible;
                    foreach (Movie m in results)
                    {
                        //display the name and the release year in parenthesis
                        ddlName.Items.Add(m.Title + " (" + m.ReleaseDate.Substring(0, 4) + ")");
                        ddlName.PlaceholderText = "Results";
                    }
                }
                //otherwise display a messsage to the user
                else
                {
                    errorMessage.Visibility = Visibility.Visible;
                    errorMessage.Text = "No Movies Found\n Try searching by ID";
                }

                response.Dispose();
            }
            catch (WebException ex)
            {
                //if connection failed, output message to user
                errorMessage.Visibility = Visibility.Visible;
                errorMessage.Text = "Failed to connect to server\nPlease check your internet connection";
            }
            pBarMainPage.IsActive = false;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void ddlName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //use combo box to drive navigation to results page
            ComboBox combo = (ComboBox)sender;
            goToResults(results[combo.SelectedIndex].Id.ToString());
        }

        private void btnID_Click(object sender, RoutedEventArgs e)
        {
            //on id button click, use the textbox value to drive navigation to the results page
            goToResults(tbID.Text);
        }
        public void goToResults(String id)
        {
            /*
                Attempt to parse the value id to an integer, on success, client is moved to the results page, where that id is 
                    used to find similar movies
                On failure, the user is given an error message detailing the problem
            */

            errorMessage.Visibility = Visibility.Collapsed;
            try
            {
                App.searchID = Convert.ToInt32(id);
                this.Frame.Navigate(typeof(Results));
            }
            catch (FormatException ex)
            {
                errorMessage.Visibility = Visibility.Visible;
                errorMessage.Text = "Failed to parse ID\nPlease enter valid number";
            }
        }

        private void tbName_GotFocus(object sender, RoutedEventArgs e)
        {
            //When the textbox gets focus, the current text is removed
            TextBox tb = (TextBox)sender;
            String s = tb.Text;
            
            tb.Text = "";
         
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
