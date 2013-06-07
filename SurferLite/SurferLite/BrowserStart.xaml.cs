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
using System.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SurferLite
{
    /// <summary>
    /// The page which is shown first on the start of the application.
    /// /// </summary>
    public sealed partial class BrowserStart : Page
    {
        // Reference to the WCF service
        ServiceReferenceForTest.Service1Client client;

        // Saves Root Url for later use to concatinate with relative paths
        string currentRootUrl;

        public BrowserStart()
        {
            this.InitializeComponent();

            // Initialize the reference object with a service
            try{
                client = new ServiceReferenceForTest.Service1Client();
            }
            catch{
                WebViewBrowse.NavigateToString("Can't connect to the SurferLite server");
            }

            // Source Code view is not visible on start of page.
            TextBlockSource.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // First page which is automatically loaded on Page start
            NavigateThroughSurferLite("http://www.bing.com");            
        }

        /// <summary>
        /// Invoked when the compressed bytes are to be decompressed for use.
        /// </summary>
        /// <param name="compressedByte">It is the compressed byte array.</param>
        /// <returns>A decompressed byte in the form of string</returns>
        private string DecompressBytes(byte[] compressedByte)
        {
            // for holding decompressed byte
            string result;
            
            //Prepare for decompress
            MemoryStream ms = new MemoryStream(compressedByte);
            GZipStream compressor = new GZipStream(ms, CompressionMode.Decompress);

            //Reset variable to collect uncompressed result
            compressedByte = new byte[9999999];

            //Decompress and rByte is size of decompressed stream
            int rByte = compressor.Read(compressedByte, 0, compressedByte.Length);

            //CompressedByte is now decompressed
            MemoryStream decompressedStream = new MemoryStream(compressedByte);

            //converting to string to return
            StreamWriter writer = new StreamWriter(decompressedStream);
            writer.Write(compressedByte);
            writer.Flush();

            decompressedStream.Position = 0;
            StreamReader reader = new StreamReader(decompressedStream);
            result = reader.ReadToEnd();

            //XXXXXXXXX: TextBlockSize.Text = result.Length.ToString()+" B"; //This gives size of decompressedStream which is 9999999
            TextBlockAnsSize.Text = rByte.ToString()+" B";
            
            // Cleanup if they are to be used again
            compressor.Dispose();
            ms.Dispose();
            
            return result;

            ////Uncomment these if you want to use String instead of string (Some modification have to be done may be)
            
            ////Transform byte[] unzip data to string
            //System.Text.StringBuilder sB = new System.Text.StringBuilder(rByte);
            
            ////Read the number of bytes GZipStream red and do not a for each bytes in
            ////resultByteArray;
            
            //for (int i = 0; i < rByte; i++)
            //{
            //    sB.Append((char)compressedByte[i]);
            //}

            //compressor.Dispose();
            //ms.Dispose();
            //TextBlockAnsSize.Text = sB.Length.ToString()+" B";

            //return sB.ToString();

        }

        /// <summary>
        /// This method is async method and is used to display page through SurferLite Service
        /// </summary>
        /// <param name="URLString">The URL in string which you want to display in WebViewBrowse</param>
        private async void NavigateThroughSurferLite(string URLString)
        {
            try
            {
                Uri URL = new Uri(URLString);
                ProgressRingLoad.IsActive = true;

                //Although Service Contract says GetHtml(URL) returns stream. It automatically is byte[].
                //XXXXXX: Stream pullStream = await client.GetHtmlAsync(URL);
                
                byte[] pullStream = await client.GetHtmlAsync(URL);
                                
                // Displaying size of byte[] got from service
                TextBlockSize.Text = pullStream.Length.ToString()+" B";
                
                // Decompressing pullStream to get actual decompressed data
                string result=DecompressBytes(pullStream);

                // To view in Html Source
                TextBlockSource.Text = result;

                // result is ready and is displayed in webview
                WebViewBrowse.NavigateToString(result);
            }

            catch(Exception e)
            {
                // There is some error. This needs modification so Exception is exact
                WebViewBrowse.NavigateToString(e.ToString());
            }

            ProgressRingLoad.IsActive = false;

        }

        /// <summary>
        /// Invoked when user press "Enter" on URL textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void KeyUpEnter(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                // Navigate to the given url through Service
                NavigateThroughSurferLite(TextBoxUrl.Text);

                // Get list of hrefs in the requested page from server
                ListBoxUrls.ItemsSource = await client.GetHrefLinksAsync(TextBoxUrl.Text);

                //Save original Text as root url for relative paths
                currentRootUrl = TextBoxUrl.Text;
            }
        }

        private async void SelectedListItem(object sender, SelectionChangedEventArgs e)
        {
            var selectedItems = ListBoxUrls.SelectedItems;
            if (selectedItems.Count > 0)
            {
                // Display text of first item selected.
                //WebViewBrowse.NavigateToString(selectedItems[0].ToString());
                if (!selectedItems[0].ToString().Contains("http"))
                {
                    TextBoxUrl.Text = currentRootUrl + selectedItems[0].ToString();
                }
                else
                {
                    TextBoxUrl.Text = selectedItems[0].ToString();
                    currentRootUrl = TextBoxUrl.Text;
                    ListBoxUrls.ItemsSource = await client.GetHrefLinksAsync(TextBoxUrl.Text);
                }
                
                NavigateThroughSurferLite(TextBoxUrl.Text);
            }
            else
            {
                // Display default string.
                WebViewBrowse.NavigateToString("Empty selected from ListBox");
            } 
        }

        private void SeeSource(object sender, RoutedEventArgs e)
        {
            TextBlockSource.Visibility = Visibility.Visible;
            ListBoxUrls.Visibility = Visibility.Collapsed;
        }

        private void CollapseSource(object sender, DoubleTappedRoutedEventArgs e)
        {
            TextBlockSource.Visibility = Visibility.Collapsed;
            ListBoxUrls.Visibility = Visibility.Visible;
        }
    }
}
