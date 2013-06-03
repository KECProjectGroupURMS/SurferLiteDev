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
    public sealed partial class PageToTestServiceOperations : Page
    {
        public PageToTestServiceOperations()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            ProgressRing1.IsActive = true;
            ServiceReferenceForTest.Service1Client client = new ServiceReferenceForTest.Service1Client();
            ListBoxMeta.ItemsSource = await client.GetDataAsync(TextBoxUrl.Text);
            ProgressRing1.IsActive = false;
        }

        private async void ButtonGetData(object sender, RoutedEventArgs e)
        {
            ProgressRing1.IsActive = true;
            ServiceReferenceForTest.Service1Client client = new ServiceReferenceForTest.Service1Client();
            //ListBoxMeta.Items.Add(await client.GetDataAsync());
            ListBoxMeta.ItemsSource = await client.GetDataAsync(TextBoxUrl.Text);
            
            //TextBlockOutput.Text = await client.GetDataAsync();
            ProgressRing1.IsActive = false;
        }
    }
}
