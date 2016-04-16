using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SuggestionsApp
{
    public sealed partial class MovieControl : UserControl
    {
        public MovieControl()
        {
            this.InitializeComponent();
            userControl.BorderBrush = new SolidColorBrush(Colors.Gray);
        }

        public String textblockTitle
        {
            get { return (String)GetValue(textblockTitleProperty); }
            set { SetValue(textblockTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for textblockTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty textblockTitleProperty =
            DependencyProperty.Register("textblockTitle", typeof(String), typeof(MovieControl), null);

        public ImageSource backSource
        {
            get { return (ImageSource)GetValue(backSourceProperty); }
            set { SetValue(backSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for textblockTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty backSourceProperty =
            DependencyProperty.Register("backSource", typeof(String), typeof(MovieControl), null);



        public String textBlockOverview
        {
            get { return (String)GetValue(textBlockOverviewProperty); }
            set { SetValue(textBlockOverviewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for textBlockOverview.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty textBlockOverviewProperty =
            DependencyProperty.Register("textBlockOverview", typeof(String), typeof(MovieControl), null);



        public String textBlockDate
        {
            get { return (String)GetValue(textBlockDateProperty); }
            set { SetValue(textBlockDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for textBlockDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty textBlockDateProperty =
            DependencyProperty.Register("textBlockDate", typeof(String), typeof(MovieControl), null);



        public String textblockRating
        {
            get { return (String)GetValue(textblockRatingProperty); }
            set { SetValue(textblockRatingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for textblockRating.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty textblockRatingProperty =
            DependencyProperty.Register("textblockRating", typeof(String), typeof(MovieControl), null);




        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(MovieControl), null);


    }
}
