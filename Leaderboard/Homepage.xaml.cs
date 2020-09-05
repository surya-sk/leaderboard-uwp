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
    /// The Homepage that the user is taken to upon app launch
    /// </summary>
    public sealed partial class Homepage : Page, IGamesUpdateable
    {
        string GameName, Type;
        double Num;
        int MaxScore;
        private ObservableCollection<Game> games;
        private ObservableCollection<GameRound> GameRounds;
        private ObservableCollection<Player> Players;
        public Homepage()
        {
            this.InitializeComponent();
            GameRounds = GameRoundsList.GetGameRounds();
            Players = PlayerList.GetPlayers();
            if(Profile.GetInstance().GetGamesList() == null)
            {
                StatusText.Text = "You currently have no games.";
            }
        }

        /// <summary>
        /// Hides the add game button after selecting it and shows the details panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputGame_Click(object sender, RoutedEventArgs e)
        {
            InputGame.Visibility = Visibility.Collapsed;
            GameDetPanel.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Take in values inputted by the user and show the pop-up to enter player names
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameName = NameInput.Text;
            Type = GameType.SelectedItem.ToString();
            string temp = NumInput.Text;
            Num = Convert.ToDouble(temp);
            string temp2 = MaxScoreInput.Text;
            MaxScore = Convert.ToInt16(temp2);
            CreatePlayerList();
            InputPlayers();
            //this.Frame.Navigate(typeof(Homepage));
            //this.Frame.Navigate(typeof(Leaderboard));
        }

        /// <summary>
        /// Creates the first round for each player
        /// </summary>
        private void CreateRoundOne()
        {
            GameRounds.Add(new GameRound { RoundName = "Round " + (GameRounds.Count + 1), score=0});
        }

        /// <summary>
        /// Create a list of players based on user input
        /// </summary>
        private void CreatePlayerList()
        {
            CreateRoundOne();
            for (int i = 0; i < Num; i++)
            {
                Players.Add(new Player { PlayerName="", GameRounds = GameRounds });
            }           
        }

        /// <summary>
        /// Shows the pop-up to enter player names
        /// </summary>
        private async void InputPlayers()
        {
            ContentDialogResult result = await NamesDialog.ShowAsync();
        }

        /// <summary>
        /// Takes player input from the pop-up and creates the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void NamesDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Game game = new Game() { Id = Guid.NewGuid(),GameName = GameName, GameType = Type, NumPlayers = Num, MaxScore = MaxScore, Players = Players } ;
            Profile.GetInstance().AddGame(game);
            InputGame.Visibility = Visibility.Visible;
            GameDetPanel.Visibility = Visibility.Collapsed;
            StatusText.Text = "Game added successfully";
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
        
        /// <summary>
        /// Prevents letters from being typed into the score box by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
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
                StatusText.Text = "Game successfully deleted";
            }
            base.OnNavigatedTo(e);
        }
    }
}
