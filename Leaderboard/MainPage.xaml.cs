﻿using System;
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
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Leaderboard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, IGamesUpdateable
    {
        private ObservableCollection<Game> games;
  
        public MainPage()
        {
            games = new ObservableCollection<Game>();
            UpdateEvent UpdateEvent = new UpdateEvent(this);
            Profile.GetInstance().ClearEvents();
            Profile.GetInstance().AddEvent(UpdateEvent);
            Profile.GetInstance().ReadProfile();
            //games = profile.GetGamesList();
            this.InitializeComponent();
        }


        private void NavigationViewItem_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(Leaderboard));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }

        public void RefreshPage()
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void NavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem SelectedItem = args.InvokedItem as NavigationViewItem;
            ContentFrame.Navigate(typeof(Leaderboard), SelectedItem.Tag);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //games = profile.GetGamesList();
            //Debug.WriteLine("On load" + games.Count);
            ContentFrame.Navigate(typeof(Homepage));
            // UpdateEvent.OnEventChanged(games);
        }

        public void UpdateGames(ObservableCollection<Game> games)
        {
            this.games = games;
            Debug.WriteLine("Mainpage" + games.Count);
        }
    }
}
