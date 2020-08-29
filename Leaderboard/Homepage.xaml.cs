using Leaderboard.Models;
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
    public sealed partial class Homepage : Page
    {
        string GameName, Type;
        double Num;
        private ObservableCollection<PlayerStat> PlayerStats;
        private ObservableCollection<Game> games;
        public Homepage()
        {
            this.InitializeComponent();
            Profile.GetInstance().ReadProfile();
            games = Profile.GetInstance().GetGamesList();
            if(games == null)
            {
                TestBlock.Text = "No Games";
            }
            else
            {
                Game TestGame;
                for (int i = 0; i < games.Count; i++)
                {
                    TestGame = games[i];
                    TestBlock.Text += TestGame.ToString();
                }
            }
            
        }

        private void InputGame_Click(object sender, RoutedEventArgs e)
        {
            InputGame.Visibility = Visibility.Collapsed;
            GameDetPanel.Visibility = Visibility.Visible;
        }

        private void CreateGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameName = NameInput.Text;
            Type = GameType.SelectedItem.ToString();
            Num = NumInput.Value;
            CreateNewGame();
            InputGame.Visibility = Visibility.Visible;
            GameDetPanel.Visibility = Visibility.Collapsed;
            this.Frame.Navigate(typeof(Homepage));
            //this.Frame.Navigate(typeof(Leaderboard));
        }

        private void CreateNewGame()
        {
            PlayerStats = PlayerStatList.GetPlayerStats();
            for (int i = 0; i < Num; i++)
            {
                PlayerStats.Add(new PlayerStat { PlayerName = "Enter name", PlayerScore = 0 });
            }
            TestBlock.Text += Num.ToString();
            Profile.id++;
            Game game = new Game() { id = Profile.id,GameName = GameName, GameType = Type, NumPlayers = Num, PlayerStatList = PlayerStats  } ;
            if (games == null)
            {
                games = new ObservableCollection<Game>();
                games.Add(game);
                Profile.GetInstance().SaveSettings(games);
                Profile.GetInstance().WriteProfile();
            }
            else
            {
                games.Add(game);
                Profile.GetInstance().SaveSettings(games);
                Profile.GetInstance().WriteProfile();
            }
        }
    }
}
