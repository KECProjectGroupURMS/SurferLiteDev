using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;

//for ObservableCollections
using System.Collections.ObjectModel;

using System.Threading;
using Windows.UI.Xaml.Media;

using Windows.UI.Xaml.Navigation;

//for tile updates
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using System.Collections.Generic;





// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Client81
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        private CustomerDepartment cusDep;
        public static List<string> history=new List<string>();

        static ObservableCollection<BookmarkItem> bookmarkss = new ObservableCollection<BookmarkItem>();
        internal static ObservableCollection<BookmarkItem> bookmarks
        {
            get { return bookmarkss; }
            set { bookmarkss = value; }
        }

        public static class Settings {
            public static bool ToggleSwitchCompression;
            public static bool ToggleSwitchImage;
            public static bool ToggleSwitchScript;
        }

        public MainPage()
        {
            this.InitializeComponent();
                        
            ////for tile
            //XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150BlockAndText01);

            //XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            //tileTextAttributes[0].InnerText = "Hello World! My very own tile notification";
            //tileTextAttributes[1].InnerText = "Test is this.";

            //TileNotification tileNotification = new TileNotification(tileXml);

            //tileNotification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(2);

            //TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
            /////////////

            //

            WriteOnTile("Surferlite is currently on");
            
            

            Windows.UI.ApplicationSettings.SettingsPane.GetForCurrentView().CommandsRequested += MainPage_CommandsRequested;
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            WebViewContentSecond.Visibility = Visibility.Collapsed;
            WebViewContentThird.Visibility = Visibility.Collapsed;
            WebViewContentFourth.Visibility = Visibility.Collapsed;
            
        }

        public static void WriteSettings()
        {
            var applicationData = Windows.Storage.ApplicationData.Current;

            var roamingSettings = applicationData.RoamingSettings;

            // Create a simple setting

            roamingSettings.Values["serverOnorOff"] = Settings.ToggleSwitchCompression;
            roamingSettings.Values["Image"] = Settings.ToggleSwitchImage;
            roamingSettings.Values["Script"] = Settings.ToggleSwitchScript;

            
        }

        private static void ReadSettings()
        {
            var applicationData = Windows.Storage.ApplicationData.Current;

            var roamingSettings = applicationData.RoamingSettings;
            // Read data from a simple setting

            if (roamingSettings.Values.Count == 3)
            {
                Settings.ToggleSwitchCompression = (bool)roamingSettings.Values["serverOnorOff"];
                Settings.ToggleSwitchImage = (bool)roamingSettings.Values["Image"];
                Settings.ToggleSwitchScript = (bool)roamingSettings.Values["Script"];
            }
        }

        private void WriteOnTile(string p)
        {
            // create a string with the tile template xml
            string tileXmlString = "<tile>"
                              + "<visual version='2'>"
                              + "<binding template='TileSquare150x150Text04' fallback='TileSquareText04'>"
                              + "<text id='1'>"+p+"</text>"
                              + "</binding>"
                              + "<binding template='TileWide310x150Text03' fallback='TileWideText03'>"
                              + "<text id='1'>" + p + "</text>"
                              + "</binding>"
                              + "<binding template='TileSquare310x310Text05'>"
                              + "<text id='1'>" + p + "</text>"
                              + "</binding>"
                              + "<binding template='TileWide310x150BlockAndText01'>"
                              + "<text id='1'>" + p + "</text>"
                              + "</binding>"
                              + "</visual>"
                              + "</tile>";

            // create a DOM
            Windows.Data.Xml.Dom.XmlDocument tileDOM = new Windows.Data.Xml.Dom.XmlDocument();
            // load the xml string into the DOM, catching any invalid xml characters 
            tileDOM.LoadXml(tileXmlString);

            try
            {
                // create a tile notification
                TileNotification tile = new TileNotification(tileDOM);

                // Enter expiry time
                tile.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(10);

                // send the notification to the app's application tile
                TileUpdateManager.CreateTileUpdaterForApplication().Update(tile);
            }
            catch (System.Exception ex)
            {
                
            }
        }

        private WebView CurrentWebView()
        {
            if (WebViewContent.Visibility == Visibility.Visible)
            {
                return WebViewContent;
            }
            else return WebViewContentSecond;
        }

        private void MainPage_CommandsRequested(Windows.UI.ApplicationSettings.SettingsPane sender, Windows.UI.ApplicationSettings.SettingsPaneCommandsRequestedEventArgs args)
        {
            Windows.UI.ApplicationSettings.SettingsCommand generalSetting =
        new Windows.UI.ApplicationSettings.SettingsCommand("AppSettings", "SurferLite Settings", (handler) =>
        {
            SettingsFlyoutGeneral generalSettingsFlyout = new SettingsFlyoutGeneral();
            generalSettingsFlyout.Show();

        });

            args.Request.ApplicationCommands.Add(generalSetting);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if (e.Parameter.ToString() != "")
                {
                    string name = e.Parameter as string;
                    TextBoxUrl.Text = name;
                    cusDep.stringURL = name;
                    Browse();
                    //WebViewContent.Navigate(new Uri(name));
                }
            }
            catch
            {

            }
            try
            {
                MainPage.ReadSettings();
            }
            catch
            {
                TextBoxUrl.Text = "Error in Reading Settings.";
            }
        }

        private async void AppBarButtonGo_Click(object sender, RoutedEventArgs e)
        {
            await Browse();
        }

        private void NavigateInWebview()
        {
            try
            {
                //For direct navigation
                //WebViewContent.Navigate(new Uri(cusDep.stringURL));

                //Navigation to decompressed string
                CurrentWebView().NavigateToString(cusDep.DecompressedData);
                
                
            }
            catch
            {
                CurrentWebView().NavigateToString("Connection Error");
            }
        }
        
        private void ProgressStart()
        {
            ProgressRingBrowse.IsActive = true;
            RectangleProgress.Margin = new Thickness(0, 0, 700, 0);
        }

        private void ProgressStop()
        {
            ProgressRingBrowse.IsActive = false;
            RectangleProgress.Margin = new Thickness(0, 0, 0, 0);
        }

        private async void TextBoxUrl_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                await Browse();
            }
        }

        private void AppBarButtonBack_Click(object sender, RoutedEventArgs e)
        {
            //WebViewContent.Navigate(new Uri("http://www.bing.com"));
            if (CurrentWebView().CanGoBack)
            {
                CurrentWebView().GoBack();
                TextBoxUrl.Text = CurrentWebView().Source.AbsoluteUri;
            }
            //CurrentWebView().GoBack();
        }

        private void AppBarButtonForward_Click(object sender, RoutedEventArgs e)
        {
            //if (WebViewContent.CanGoForward) WebViewContent.GoForward();
            //WebViewContent.Navigate(new Uri("http://www.bing.com"));
            if (CurrentWebView().CanGoForward)
            {
                CurrentWebView().GoForward();
                TextBoxUrl.Text = CurrentWebView().Source.AbsoluteUri;
            }
            //CurrentWebView().GoBack();
        }

        private async void AppBarButtonBookmarks_Click(object sender, RoutedEventArgs e)
        {
            InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
            await CurrentWebView().CapturePreviewToStreamAsync(ms);

            //Create a small thumbnail
            int longlength = 180, width = 0, height = 0;
            double srcwidth = CurrentWebView().ActualWidth, srcheight = CurrentWebView().ActualHeight;
            double factor = srcwidth / srcheight;
            if (factor < 1)
            {
                height = longlength;
                width = (int)(longlength * factor);
            }
            else
            {
                width = longlength;
                height = (int)(longlength / factor);
            }
            BitmapSource small = await resize(width, height, ms);

            BookmarkItem item = new BookmarkItem();
            item.Title = CurrentWebView().DocumentTitle;

            try
            {
                item.PageUrl = new Uri(TextBoxUrl.Text);
            }
            catch
            {

            }
            //item.PageUrl = CurrentWebView().Source;
            
            item.Preview = small;

            bookmarks.Add(item);
        }

        async Task<BitmapSource> resize(int width, int height, Windows.Storage.Streams.IRandomAccessStream source)
        {
            WriteableBitmap small = new WriteableBitmap(width, height);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(source);
            BitmapTransform transform = new BitmapTransform();
            transform.ScaledHeight = (uint)height;
            transform.ScaledWidth = (uint)width;
            //
            //transform.InterpolationMode = BitmapInterpolationMode.NearestNeighbor;
            //
            PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Straight,
                transform,
                ExifOrientationMode.RespectExifOrientation,
                ColorManagementMode.DoNotColorManage);
            pixelData.DetachPixelData().CopyTo(small.PixelBuffer);
            return small;
        }

        private void AppBarButtonBookmarkPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageBookmarks));
        }

        private async void AppBarButtonReqPageSize_Click(object sender, RoutedEventArgs e)
        {
            ////CallerDepartment call = new CallerDepartment();
            ////call.SendRequest(TextBoxUrl.Text);
            ////call.SaveDataToCloud();
            ////TextBlockSize.Text = call.receivedData.ToString();
            //try
            //{
            //    client = new ServiceReferenceAzureLocal.ServiceSurferliteClient();
            //    //client = new ServiceReferenceAzure.ServiceSurferliteClient();
            //}
            //catch
            //{
            //   WebViewContent.NavigateToString("Can't connect to the SurferLite server");
            //}

            //if (client != null)
            //{
            //    ProgressStart();
            //    try
            //    {
            //        byte[] newSt = await client.GetDataAsync(new Uri("http://www.bing.com/"));
            //        TextBlockSize.Text = newSt.Length.ToString();

            //        string ans = await client.SaveDataToCloudAsync("Log");
            //    }
            //    catch (Exception f)
            //    {
            //        // There is some error. This needs modification so Exception is exact
            //        WebViewContent.NavigateToString(f.ToString());
            //    }
            //    ProgressStop();

            //}
            string newstring = "<!DOCTYPE html><html><head>    <title>Example HTML document</title>    <script type=\"text/javascript\">        function SendBlue() {            window.external.notify('blue');        }        function SendGreen() {            window.external.notify('green');        }    </script></head><body>    <h1>Hi!</h1>    <p>This is a simple test page for window.external.notify(). When you click on either of the buttons below it will send a notification to the host.</p>    <button onclick=\"window.external.notify('blue');\">Send Blue</button>    <button onclick=\"SendGreen()\">Send Green</button><a href=\"#\" onclick=\"window.external.notify('blue');\">LINK</a><button onclick=\"window.external.notify('test')\" >Click Here!</button></body></html>";
            CurrentWebView().NavigateToString(newstring);
            CurrentWebView().NavigationStarting -= WebViewContent_NavigationStarting;

        }

        private async Task Browse()
        {
            ProgressRingBrowse.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            ProgressStart();

            cusDep = new CustomerDepartment();
            if (!(TextBoxUrl.Text.Contains("http://") || TextBoxUrl.Text.Contains("https://") || TextBoxUrl.Text.Contains(".com")))
            {
                cusDep.stringURL = "http://www.google.com/search?q=" + TextBoxUrl.Text;
            } else cusDep.stringURL = TextBoxUrl.Text;
            try
            {
                if (Settings.ToggleSwitchCompression == true)
                {
                    await cusDep.GetUri();
                    NavigateInWebview();
                    TextBoxUrl.Text = cusDep.stringURL;
                }
                else
                {
                    CurrentWebView().Navigate(new Uri(cusDep.stringURL));
                }
                
            }
            catch (Exception e)
            {
                CurrentWebView().NavigateToString(e.ToString());
            }

            
        }

        private async void Browse(string url)
        {
            ProgressRingBrowse.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            ProgressStart();

            cusDep = new CustomerDepartment();
            cusDep.stringURL = url;
            try
            {
                await cusDep.GetUri();
                NavigateInWebview();
                TextBoxUrl.Text = cusDep.stringURL;
            }
            catch (Exception e)
            {
                CurrentWebView().NavigateToString(e.ToString());
            }
        }

        private void AppBarButtonStop_Click(object sender, RoutedEventArgs e)
        {
            CurrentWebView().Stop();
            ProgressStop();
        }

        private void WebViewContent_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            //cusDep.browseStatus = "Page Load Completed.";
            //TextBlockStatus.Text = cusDep.browseStatus;
            ProgressStop();
        }

        private void WebViewContent_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            ProgressRingBrowse.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            ProgressStart();

            //TextBoxUrl.Text = CurrentWebView().Source.AbsoluteUri;
            //cusDep.browseStatus = "Getting Page";
            //TextBlockStatus.Text = cusDep.browseStatus;
        }

        private void WebViewContent_ScriptNotify(object sender, NotifyEventArgs e)
        {
            Browse(e.Value);
        }

        private void AppBarButtonSettings_Click(object sender, RoutedEventArgs e)
        {

            SettingsFlyoutGeneral updatesFlyout = new SettingsFlyoutGeneral();
            updatesFlyout.ShowIndependent();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //CurrentWebView().Refresh();
            Browse();
        }

        private void AppBarNewTab_Click(object sender, RoutedEventArgs e)
        {
            WebViewContent.Visibility = Visibility.Collapsed;
            WebViewContentSecond.Visibility = Visibility.Visible;
            try
            {
                TextBoxUrl.Text = CurrentWebView().Source.AbsoluteUri;
            }
            catch { }
        }

        private void AppBarOldTab_Click(object sender, RoutedEventArgs e)
        {
            WebViewContentSecond.Visibility = Visibility.Collapsed;
            WebViewContent.Visibility = Visibility.Visible;
            try
            {
                TextBoxUrl.Text = CurrentWebView().Source.AbsoluteUri;
            }
            catch { }
        }

        private void WebViewContent_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        {
            try
            {
                TextBoxUrl.Text = CurrentWebView().Source.AbsoluteUri;
            }
            catch{
                try
                {
                    TextBoxUrl.Text = cusDep.stringURL;
                }
                catch
                {
                    TextBoxUrl.Text = "Command Not available now";
                }
            }
            
            //Add to history list
            history.Add(TextBoxUrl.Text);

            WriteOnTile("Current URL: " + TextBoxUrl.Text);
        }

        private void AppBarButtonSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

    
