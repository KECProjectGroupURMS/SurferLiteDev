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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Client81
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CustomerDepartment cusDep;

        public MainPage()
        {
            this.InitializeComponent();

            WebViewContent.LoadCompleted += Completed;
            WebViewContent.NavigationStarting += WebViewContent_NavigationStarting;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ProgressStart();
            cusDep = new CustomerDepartment();
            cusDep.stringURL = TextBoxUrl.Text;
            cusDep.GetUri();
            NavigateInWebview();
        }

        private void NavigateInWebview()
        {
            try
            {
                //WebViewContent.Navigate(new Uri(cusDep.stringURL));
                if (cusDep.dataReceived == "NoNetwork")
                {
                    WebViewContent.Navigate(new Uri(cusDep.stringURL));
                }
                WebViewContent.NavigateToString(cusDep.dataReceived);
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

        private void Completed(object sender, NavigationEventArgs e)
        {
            cusDep.browseStatus = "Page Load Completed.";
            TextBlockStatus.Text = cusDep.browseStatus;
            ProgressStop();
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
                cusDep = new CustomerDepartment();
                cusDep.GetUri();
                NavigateInWebview();
            }
        }
    }
}
