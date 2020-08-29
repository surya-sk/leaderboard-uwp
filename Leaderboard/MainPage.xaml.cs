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
using Leaderboard.Models;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Leaderboard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Profile profile = Profile.GetInstance();
        private ObservableCollection<Game> games;
        public MainPage()
        {
            profile.ReadProfile();
            games = profile.GetGamesList();
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(Homepage));
        }

        private void HomeButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(Homepage));
            
        }


        private void NavigationView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if(args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(Settings));
            }
        }

        private void NavigationViewItem_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(Leaderboard));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
