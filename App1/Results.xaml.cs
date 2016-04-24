using Newtonsoft.Json;
using SuggestionsApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class Results : Page
    {
        List<Movie> results = new List<Movie>();
        public Results()
        {
            this.InitializeComponent();
        }
        /*
         *  When the results page is navigated to, a get request is sent to the Heroku server containing the ID of the selected movie.
         */
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            pBarResultPage.IsActive = true;
            int rows;
            try
            {
                //makes a get request to the heroku server, sets up the global variable results
                await makeGETRequest();

                MovieControl mc;
                PivotItem p;
                /*
                    Loops through every movie object in results.
                    Creates a MovieControl with the data from the current Movie object.
                    Also adding a pivot item, setting its content to the MovieControl
                        setting its header to the name of the movie. 
                    Add the pivot item to the XAML pivot control
                */
                for (rows = 0; rows < results.Count; rows++)
                {
                    mc = new MovieControl();
                    //mc.textblockTitle = results[rows].Title;
                    mc.ImageSource = new BitmapImage(new Uri("https://image.tmdb.org/t/p/w396" + results[rows].Poster));
                    mc.textBlockOverview = results[rows].Overview;
                    mc.textblockRating = "Average Score: " + results[rows].VoteAvg;
                    mc.textBlockDate = "Release Date: " + results[rows].ReleaseDate;

                    mc.backSource = new BitmapImage(new Uri("https://image.tmdb.org/t/p/w396" + results[rows].BackDrop));
                    mc.SetValue(Grid.RowProperty, rows);
                    mc.HorizontalAlignment = HorizontalAlignment.Stretch;
                    p = new PivotItem();
                    p.Header = results[rows].Title;
                    p.Foreground = new SolidColorBrush(Colors.White);
                    p.Content = mc;
                    pivoter.Items.Add(p);
                }
                pBarResultPage.IsActive = false;
            }
            catch
            {
                //If the get request times out, display the error message on the mainpage, return to the mainpage
                App.errorMsg = "Server Timed out\nPlease try again";
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        public async Task makeGETRequest()
        {
            /*
                Make a get request to the heroku server. Read in the returned json. 
                Create movie objects from json. Store movie objects in results. 
            */
            string uri = "http://localhost:4567/request/movie/" + App.searchID;
            WebRequest wrGETURL = WebRequest.Create(uri);
            wrGETURL.Proxy = null;
            try
            {
                WebResponse response = await wrGETURL.GetResponseAsync();
                Stream dataStream = response.GetResponseStream();
                StreamReader objReader = new StreamReader(dataStream);
                dynamic movie = JsonConvert.DeserializeObject(objReader.ReadToEnd());
                results = new List<Movie>();
                foreach (dynamic results in movie)
                {
                    this.results.Add(new Movie((int)results.id, System.Convert.ToString(results.poster_path), System.Convert.ToString(results.title), System.Convert.ToString(results.overview), System.Convert.ToString(results.release_date), System.Convert.ToString(results.vote_average), System.Convert.ToString(results.backdrop_path), (int)results.Score));
                }
                results = results.OrderByDescending(m => m.SuggestionScore).ToList();
                response.Dispose();
            }
            catch (WebException ex)
            {
                App.errorMsg = "Server Error\nPlease try again";
                this.Frame.Navigate(typeof(MainPage));
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //on cick, go back to mainPage
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}