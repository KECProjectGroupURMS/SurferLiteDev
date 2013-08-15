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
    public sealed partial class BrowserForwardBackward : Page
    {
        public BrowserForwardBackward()
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
        }

        private void Gobtn_Click(object sender, RoutedEventArgs e)
        {
            Uri targetUri = new Uri(Addressbar.Text);
            WebView1.Navigate(targetUri);
        }

        private void Google_Checked(object sender, RoutedEventArgs e)
        {

            Uri targetUri = new Uri(@"http://www.google.com");
            WebView1.Navigate(targetUri);
        }

        private void Facebook_Checked(object sender, RoutedEventArgs e)
        {
            Uri targetUri = new Uri(@"http://www.facebook.com");
            WebView1.Navigate(targetUri);
        }

        private void Twitter_Checked(object sender, RoutedEventArgs e)
        {
            Uri targetUri = new Uri(@"http://www.twitter.com");
            WebView1.Navigate(targetUri);
        }

        private void Hotmail_Checked(object sender, RoutedEventArgs e)
        {
            Uri targetUri = new Uri(@"http://www.hotmail.com");
            WebView1.Navigate(targetUri);
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            //invoking javascript to go back
            WebView1.InvokeScript("eval", new[] { "(function(){ history.go(-1);})()" });
        }

        private void forwardbtn_Click(object sender, RoutedEventArgs e)
        {
            //invoking javascript to go forward
            WebView1.InvokeScript("eval", new[] { "(function(){ history.go(1);})()" });
        }

        private void refreshbtn_Click(object sender, RoutedEventArgs e)
        {
            WebView1.Navigate(new Uri(Addressbar.Text));
        }
    }
}
