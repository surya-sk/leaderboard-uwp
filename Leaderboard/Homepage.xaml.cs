using Leaderboard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public sealed partial class Homepage : Page, IGamesUpdateable
    {
        string GameName, Type;
        double Num;
        int MaxScore;
        private ObservableCollection<PlayerStat> PlayerStats;
        private ObservableCollection<Game> games;
        private ObservableCollection<GameRound> GameRounds;
        public Homepage()
        {
            this.InitializeComponent();
            PlayerStats = PlayerStatList.GetPlayerStats();
            GameRounds = GameRoundsList.GetGameRounds();
            //UpdateEvent updateEvent = new UpdateEvent(this);
            //Profile.GetInstance().AddEvent(updateEvent);
            //Profile.GetInstance().ReadProfile();
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
            string temp = NumInput.Text;
            Num = Convert.ToDouble(temp);
            string temp2 = MaxScoreInput.Text;
            MaxScore = Convert.ToInt16(temp2);
            InputPlayers();
            CreateRoundOne();
            //this.Frame.Navigate(typeof(Homepage));
            //this.Frame.Navigate(typeof(Leaderboard));
        }

        private void CreateRoundOne()
        {
            CreatePlayerStats();
            GameRounds.Add(new GameRound { RoundName = "Round " + (GameRounds.Count - 1), MaxScore = MaxScore, PlayerStats = PlayerStats });
        }

        private void CreatePlayerStats()
        {
            for (int i = 0; i < Num; i++)
            {
                PlayerStats.Add(new PlayerStat { PlayerName = "", PlayerScore = 0 });
            }           
        }

        private async void InputPlayers()
        {
            ContentDialogResult result = await NamesDialog.ShowAsync();
        }

        private void NamesDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Game game = new Game() { Id = Guid.NewGuid(),GameName = GameName, GameType = Type, NumPlayers = Num, GameRoundsList = GameRounds } ;
            Profile.GetInstance().AddGame(game);
            InputGame.Visibility = Visibility.Visible;
            GameDetPanel.Visibility = Visibility.Collapsed;
        }


        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HelpPage));
        }

        public void UpdateGames(ObservableCollection<Game> games)
        {
            this.games = games;
            Debug.WriteLine("Homepage" + games.Count);
        }

        private void NumInput_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            foreach (char c in args.NewText)
            {
                if (!char.IsDigit(c))
                {
                    args.Cancel = true;
                }
            }
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string result = (string)e.Parameter;
            if(result == "deleted")
            {
                GameDeleted.Text = "Game successfully deleted";
            }
            base.OnNavigatedTo(e);
        }
    }
}
