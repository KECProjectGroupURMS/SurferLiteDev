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

//for colors

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Client81
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CustomerDepartment cusDep;

        static ObservableCollection<BookmarkItem> bookmarkss = new ObservableCollection<BookmarkItem>();
        internal static ObservableCollection<BookmarkItem> bookmarks
        {
            get { return bookmarkss; }
            set { bookmarkss = value; }
        }

        public static class Settings {
            public static bool ToggleSwitchCompression=false;
            public static bool ToggleSwitchImage = false;
            public static bool ToggleSwitchScript = false;
        }

        public MainPage()
        {
            this.InitializeComponent();
            

            Windows.UI.ApplicationSettings.SettingsPane.GetForCurrentView().CommandsRequested += MainPage_CommandsRequested;
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            WebViewContent_Copy.Visibility = Visibility.Collapsed;
            WebViewContent_Copy2.Visibility = Visibility.Collapsed;
            WebViewContent_Copy3.Visibility = Visibility.Collapsed;
            
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
                if (WebViewContent.Visibility == Visibility.Visible)
                {
                    WebViewContent.NavigateToString(cusDep.DecompressedData);
                }
                else WebViewContent_Copy.NavigateToString(cusDep.DecompressedData);
                
                
            }
            catch
            {
                if (WebViewContent.Visibility == Visibility.Visible)
                {
                    WebViewContent.NavigateToString("Connection Error");
                }else WebViewContent_Copy.NavigateToString("Connection Error");
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
            if (WebViewContent.CanGoBack)
            {
                WebViewContent.GoBack();
                TextBoxUrl.Text = WebViewContent.Source.AbsoluteUri;
            }
            //WebViewContent.GoBack();
        }

        private void AppBarButtonForward_Click(object sender, RoutedEventArgs e)
        {
            //if (WebViewContent.CanGoForward) WebViewContent.GoForward();
            //WebViewContent.Navigate(new Uri("http://www.bing.com"));
            if (WebViewContent.CanGoForward)
            {
                WebViewContent.GoForward();
                TextBoxUrl.Text = WebViewContent.Source.AbsoluteUri;
            }
            //WebViewContent.GoBack();
        }

        private async void AppBarButtonBookmarks_Click(object sender, RoutedEventArgs e)
        {
            InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream();
            await WebViewContent.CapturePreviewToStreamAsync(ms);

            //Create a small thumbnail
            int longlength = 180, width = 0, height = 0;
            double srcwidth = WebViewContent.ActualWidth, srcheight = WebViewContent.ActualHeight;
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
            item.Title = WebViewContent.DocumentTitle;

            try
            {
                item.PageUrl = new Uri(TextBoxUrl.Text);
            }
            catch
            {

            }
            //item.PageUrl = WebViewContent.Source;
            
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
            WebViewContent.NavigateToString(newstring);
            WebViewContent.NavigationStarting -= WebViewContent_NavigationStarting;

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
                    if (WebViewContent.Visibility == Visibility.Visible)
                    {
                        WebViewContent.Navigate(new Uri(cusDep.stringURL));
                    }

                    else
                    {
                        WebViewContent_Copy.Navigate(new Uri(cusDep.stringURL));
                    }
                    
                }
                
            }
            catch (Exception e)
            {
                if (WebViewContent.Visibility == Visibility.Visible)
                    WebViewContent.NavigateToString(e.ToString());
                else WebViewContent_Copy.NavigateToString(e.ToString());
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
                WebViewContent.NavigateToString(e.ToString());
            }
        }

        private void AppBarButtonStop_Click(object sender, RoutedEventArgs e)
        {
            WebViewContent.Stop();
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

            if (WebViewContent.Visibility == Visibility.Visible)
            {
                TextBoxUrl.Text = WebViewContent.Source.AbsoluteUri;
            }
            else TextBoxUrl.Text = WebViewContent_Copy.Source.AbsoluteUri;
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
            //WebViewContent.Refresh();
            Browse();
        }

        private void AppBarNewTab_Click(object sender, RoutedEventArgs e)
        {
            WebViewContent.Visibility = Visibility.Collapsed;
            WebViewContent_Copy.Visibility = Visibility.Visible;
            try
            {
                TextBoxUrl.Text = WebViewContent_Copy.Source.AbsoluteUri;
            }
            catch { }
        }

        private void AppBarOldTab_Click(object sender, RoutedEventArgs e)
        {
            WebViewContent_Copy.Visibility = Visibility.Collapsed;
            WebViewContent.Visibility = Visibility.Visible;
            try
            {
                TextBoxUrl.Text = WebViewContent.Source.AbsoluteUri;
            }
            catch { }
        }
    }
}

    
