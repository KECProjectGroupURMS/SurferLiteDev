﻿using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SurferLite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BrowserStart : Page
    {
        public BrowserStart()
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
            ProgressRingLoad.IsActive = true;
            ServiceReferenceForTest.Service1Client client = new ServiceReferenceForTest.Service1Client();
          
            byte[] pullStream= await client.GetHtmlAsync();
            
            MemoryStream theMemStream = new MemoryStream();

            theMemStream.Write(pullStream, 0, pullStream.Length);
            //StreamReader pullStreamReader = new StreamReader(pullStream);
            ProgressRingLoad.IsActive = false;
        }
    }
}
