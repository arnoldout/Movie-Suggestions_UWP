using Newtonsoft.Json;
using SuggestionsApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            int rows;
            await makeGETRequest();
            MovieControl mc;
            /*for (rows = 0; rows < results.Count; rows++)
            {
                //contentGrid.RowDefinitions.Add(new RowDefinition());
            }*/
            PivotItem p;
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
                pivoter.Items.Add(p);
                p.Content = mc;
            }
        }

        public async Task makeGETRequest()
        {
            string uri = "https://matchingsocks.herokuapp.com/request/movie/" + App.searchID;
            WebRequest wrGETURL = WebRequest.Create(uri);
            wrGETURL.Proxy = null;
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
            results = results.OrderByDescending(m => m.VoteAvg).ToList();
            response.Dispose();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}