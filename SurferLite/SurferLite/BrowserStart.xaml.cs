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
                //Stream pullStream = await client.GetHtmlAsync(URL);
                
                
                TextBlockSize.Text = pullStream.Length.ToString()+" B";

                //CopiedFromNet: System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArray);
                MemoryStream theMemStream = new MemoryStream(pullStream);
                //NOTNEEDED: theMemStream.Write(pullStream, 0, pullStream.Length);

                //Decompress the stream
                MemoryStream decompressedStream = new MemoryStream();
                ////getting ready for source
                //var sourceStream = new MemoryStream();
                //theMemStream.CopyTo(sourceStream);
                //sourceStream.Close();


                ////converting to byte[]
                //byte[] newByteArray = sourceStream.ToArray();
                
                //make new compressor "zip"
                GZipStream zip = new GZipStream(theMemStream, CompressionMode.Decompress, true);
                
                //    zip.Read(pullStream, 0, pullStream.Length);
                

                pullStream = new byte[pullStream.Length];
                int rByte = zip.Read(pullStream, 0, pullStream.Length);
                
                //zip.Close();
                ////////////////////////////////////
                //var theMemStream = new MemoryStream();
                //zip.CopyTo(theMemStream);
                //theMemStream.Close();
                //////////////////////////////////////
                //long a = compressedStream.Length;
                /////////////////////////////////////

                //////////////////////////////////////
                
            
                ////////////////////////

                //converting to string to show to webview
                StreamWriter writer = new StreamWriter(decompressedStream);
                writer.Write(pullStream);
                writer.Flush();
                
                decompressedStream.Position = 0;
                StreamReader reader = new StreamReader(decompressedStream);
                string result = reader.ReadToEnd();
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
