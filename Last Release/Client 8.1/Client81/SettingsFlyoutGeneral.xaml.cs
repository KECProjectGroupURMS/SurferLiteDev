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

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace Client81
{
    public sealed partial class SettingsFlyoutGeneral : SettingsFlyout
    {
        public SettingsFlyoutGeneral()
        {
            this.InitializeComponent();
            this.ToggleSwitchCompression.IsOn = MainPage.Settings.ToggleSwitchCompression;
            this.ToggleSwitchImage.IsOn = MainPage.Settings.ToggleSwitchImage;
            this.ToggleSwitchScripts.IsOn = MainPage.Settings.ToggleSwitchScript;
        }

        private void ToggleSwitchCompression_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch t = sender as ToggleSwitch;
            if (t.IsOn) MainPage.Settings.ToggleSwitchCompression = true;
            else MainPage.Settings.ToggleSwitchCompression = false;
        }

        private void ToggleSwitchImage_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch t = sender as ToggleSwitch;
            if (t.IsOn) MainPage.Settings.ToggleSwitchImage = true;
            else MainPage.Settings.ToggleSwitchImage = false;
        }

        private void ToggleSwitchScripts_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch t = sender as ToggleSwitch;
            if (t.IsOn) MainPage.Settings.ToggleSwitchScript = true;
            else MainPage.Settings.ToggleSwitchScript = false;
        }
    }
}
