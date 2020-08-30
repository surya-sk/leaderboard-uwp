using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaderboard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Leaderboard : Page
    {
        private ObservableCollection<PlayerStat> playerStats;
        private ObservableCollection<Game> games;
        public Leaderboard()
        {
            games = Profile.GetInstance().GetGamesList();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Guid guid = (Guid)e.Parameter;
            for(int i=0;i<games.Count;i++)
            {
                if(games[i].id == guid)
                {
                    playerStats = games[i].PlayerStatList;
                    GameName.Text = games[i].GameName;
                    GameType.Text = games[i].GameType;
                }
            }
            base.OnNavigatedTo(e);
        }

    }
}
