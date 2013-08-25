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

//for ObservableCollections
using System.Collections.ObjectModel;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Client81
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageBookmarks : Page
    {
        
        public PageBookmarks()
        {
            this.InitializeComponent();
            this.bookmarkList.ItemsSource = MainPage.bookmarks;
            ListBoxHistory.ItemsSource = MainPage.history;
        }

        private void ButtonBackToMain_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void bookmarkList_ItemClick(object sender, ItemClickEventArgs e)
        {
            BookmarkItem b = (BookmarkItem)e.ClickedItem;
            if (b.PageUrl == null)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                this.Frame.Navigate(typeof(MainPage), b.PageUrl.AbsoluteUri);
            }
        }

        private void bookmarkList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ListBoxHistory_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

            if (ListBoxHistory.SelectedIndex==-1)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
            else
            {
                this.Frame.Navigate(typeof(MainPage), ListBoxHistory.SelectedItem.ToString());
            }
        }
    }

    class BookmarkItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private Uri _pageUrl;
        public Uri PageUrl
        {
            get { return this._pageUrl; }
            set
            {
                _pageUrl = value;
                NotifyPropertyChanged();
            }
        }

        private BitmapSource _preview;
        public BitmapSource Preview
        {
            get { return this._preview; }
            set
            {
                _preview = value;
                NotifyPropertyChanged();
            }
        }

        private String _title;
        public String Title
        {
            get { return this._title; }
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }
    }
}
