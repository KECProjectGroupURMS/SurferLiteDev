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

using System.Net;
using System.IO.Compression;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SurferLite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BrowserStart : Page
    {

        ServiceReferenceForTest.Service1Client client;

        public BrowserStart()
        {
            this.InitializeComponent();
            try{
                client = new ServiceReferenceForTest.Service1Client();
            }
            catch{
                WebViewBrowse.NavigateToString("Can't connect to the SurferLite server");
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigateThroughSurferLite("https://www.google.com/");            
        }

        private async void NavigateThroughSurferLite(string URLString)
        {
            try
            {
                Uri URL = new Uri(URLString);
                ProgressRingLoad.IsActive = true;

                byte[] pullStream = await client.GetHtmlAsync(URL);
                //GZipStream pullStream = await client.GetHtmlAsync(URL);
                
                
                TextBlockSize.Text = (pullStream.Length/1024).ToString()+" KB";
                MemoryStream theMemStream = new MemoryStream();

                theMemStream.Write(pullStream, 0, pullStream.Length);
                //converting to string to show to webview
                StreamWriter writer = new StreamWriter(theMemStream);
                writer.Write(pullStream);
                writer.Flush();

                theMemStream.Position = 0;
                StreamReader reader = new StreamReader(theMemStream);
                string result = reader.ReadToEnd();
                WebViewBrowse.NavigateToString(result);
            }
            catch
            {
                WebViewBrowse.NavigateToString("Error");
            }

            ProgressRingLoad.IsActive = false;

        }

        private void KeyUpEnter(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                NavigateThroughSurferLite(TextBoxUrl.Text);
            }
        }
    }
}
