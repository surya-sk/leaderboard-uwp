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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Leaderboard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Leaderboard : Page
    {
        double Num = Homepage.GetNum();
        string GameName = Homepage.GetGameName();
        string GameType = Homepage.GetGameType();
        private ObservableCollection<PlayerStat> PlayerStats;
        public Leaderboard()
        {
            this.InitializeComponent();
            GameTitle.Text = GameName;
            PlayerStats = PlayerStatList.GetPlayerStats();
            for(int i =0; i<Num;i++)
            {

                PlayerStats.Add(new PlayerStat { PlayerName = "Enter name", PlayerScore = Num });
            }
        }

        
        
    }
}
