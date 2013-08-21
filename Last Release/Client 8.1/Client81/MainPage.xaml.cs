using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;

//for ObservableCollections
using System.Collections.ObjectModel;

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

        public MainPage()
        {
            this.InitializeComponent();

            WebViewContent.NavigationCompleted += WebViewContent_NavigationCompleted;
            WebViewContent.NavigationStarting += WebViewContent_NavigationStarting;

            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void WebViewContent_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            cusDep.browseStatus = "Page Load Completed.";
            TextBlockStatus.Text = cusDep.browseStatus;
            ProgressStop();
        }

        private void AppBarButtonGo_Click(object sender, RoutedEventArgs e)
        {
            ProgressStart();
            cusDep = new CustomerDepartment();
            cusDep.stringURL = TextBoxUrl.Text;
            cusDep.GetUri();
            NavigateInWebview();
            TextBoxUrl.Text = cusDep.stringURL;
        }

        private void NavigateInWebview()
        {
            try
            {
                WebViewContent.Navigate(new Uri(cusDep.stringURL));
                //if (cusDep.dataReceived == "NoNetwork")
                //{
                //    WebViewContent.Navigate(new Uri(cusDep.stringURL));
                //}
                //WebViewContent.NavigateToString(cusDep.dataReceived);
            }
            catch
            {
                WebViewContent.NavigateToString("Connection Error");
            }
        }

        private void WebViewContent_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            ProgressStart();
            cusDep.browseStatus = "Getting Page";
            TextBlockStatus.Text = cusDep.browseStatus;

        }

        private void ProgressStart()
        {
            ProgressRingBrowse.IsActive = true;
        }
        private void ProgressStop()
        {
            ProgressRingBrowse.IsActive = false;
        }

        private void TextBoxUrl_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                ProgressStart();
                cusDep = new CustomerDepartment();
                cusDep.stringURL = TextBoxUrl.Text;
                cusDep.GetUri();
                TextBoxUrl.Text = cusDep.stringURL;
                NavigateInWebview();
                ProgressStop();
            }
        }

        private void AppBarButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (WebViewContent.CanGoBack) WebViewContent.GoBack();
        }

        private void AppBarButtonForward_Click(object sender, RoutedEventArgs e)
        {
            if (WebViewContent.CanGoForward) WebViewContent.GoForward();
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
            item.PageUrl = WebViewContent.Source;
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
    }
}

    
