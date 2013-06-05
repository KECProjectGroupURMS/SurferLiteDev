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
using System.IO;
using System.Text;

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
        private string DecompressBytes(byte[] compressedByte)
        {
            string result;
            
            //Ready
            //Prepare for decompress
            MemoryStream ms = new MemoryStream(compressedByte);
            GZipStream compressor = new GZipStream(ms, CompressionMode.Decompress);

            //Reset variable to collect uncompressed result
            compressedByte = new byte[9999999];

            //Decompress
            int rByte = compressor.Read(compressedByte, 0, compressedByte.Length);

            MemoryStream decompressedStream = new MemoryStream(compressedByte);

            //converting to string to return
            StreamWriter writer = new StreamWriter(decompressedStream);
            writer.Write(compressedByte);
            writer.Flush();

            decompressedStream.Position = 0;
            StreamReader reader = new StreamReader(decompressedStream);
            result = reader.ReadToEnd();
            //TextBlockAnsSize.Text = result.Length.ToString()+" B";
            TextBlockAnsSize.Text = rByte.ToString()+" B";
            

            compressor.Dispose();
            ms.Dispose();
            return result;
            
            //These works remove //s
            //////////Transform byte[] unzip data to string
            ////////System.Text.StringBuilder sB = new System.Text.StringBuilder(rByte);
            //////////Read the number of bytes GZipStream red and do not a for each bytes in
            //////////resultByteArray;
            ////////for (int i = 0; i < rByte; i++)
            ////////{
            ////////    sB.Append((char)compressedByte[i]);
            ////////}

            ////////compressor.Dispose();
            ////////ms.Dispose();
            ////////TextBlockAnsSize.Text = sB.Length.ToString()+" B";

            ////////return sB.ToString();

        }

        private async void NavigateThroughSurferLite(string URLString)
        {
            try
            {
                Uri URL = new Uri(URLString);
                ProgressRingLoad.IsActive = true;

                byte[] pullStream = await client.GetHtmlAsync(URL);
                //Stream pullStream = await client.GetHtmlAsync(URL);
                
                
                TextBlockSize.Text = pullStream.Length.ToString()+" B";

               
                string result=DecompressBytes(pullStream);
                WebViewBrowse.NavigateToString(result);
            }
            catch(Exception e)
            {
                
                WebViewBrowse.NavigateToString(e.ToString());
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
