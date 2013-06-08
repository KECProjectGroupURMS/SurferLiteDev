using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SurferLite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //string abc="<script type=\"text/javascript\" src=\"http://api.filepicker.io/v1/filepicker.js\"></script><script>filepicker.setKey(\"ArssyemSbTa6gPcnwkzXCz\")</script><script type=\"text/javascript\">filepicker.pick(function(FPFile){console.log(FPFile.url);});</script><body><input type=\"filepicker\" name=\"attachment\"><button data-fp-url=\"http://87107.zbigz.com/core/outfile.php?did=86d4ba33ee841d7943d939571e281278\">Save File</button></body>";
            //WebViewCloudDisk.NavigateToString(abc);
        }

        private void backButtonClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
