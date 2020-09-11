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
    /// The page for each of the games.
    /// </summary>
    public sealed partial class Leaderboard : Page
    {
        private ObservableCollection<Player> players;
        private ObservableCollection<Game> games;
        private Game game;
        Guid guid;
        static bool EditMode;
        public Leaderboard()
        {
            games = Profile.GetInstance().GetGamesList();
            this.InitializeComponent();
        }

        /// <summary>
        /// Takes in the guid from the clicked item from the navigation view
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            guid = (Guid)e.Parameter;
            for(int i=0;i<games.Count;i++)
            {
                if(games[i].Id == guid)
                {
                    game = games[i];
                    players = game.Players;
                    GameName.Text = game.GameName;
                }
            }
            base.OnNavigatedTo(e);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Homepage));
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

        /// <summary>
        /// Saves the current game status 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveGame();
        }

        private void SaveGame()
        {
            game.Players = players;
            for (int i = 0; i < games.Count; i++)
            {
                if (games[i].Id == guid)
                {
                    games[i] = game;
                }
            }
            Profile.GetInstance().SaveSettings(games);
            Profile.GetInstance().WriteProfile();
        }

        /// <summary>
        /// Deletes the game after warning 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(EditMode)
            {
                //Delete selected players
                for(int i = 0; i < MyListView.SelectedItems.Count; i++ )
                {
                    Player selectedPlayer = (Player)MyListView.SelectedItems[i];
                    for (int j = 0; j < players.Count; j++)
                    {
                        if (selectedPlayer == players[j])
                        {
                            players.Remove(selectedPlayer);
                        }
                    }
                }  
            }
            else
            {
                ContentDialog deleteDialog = new ContentDialog()
                {
                    Title = "Delete game permanently?",
                    Content = "If you delete this game, you cannot recover it. Are you sure?",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel"
                };
                ContentDialogResult contentDialogResult = await deleteDialog.ShowAsync();
                if (contentDialogResult == ContentDialogResult.Primary)
                {
                    DeleteGame();
                    this.Frame.Navigate(typeof(Homepage), "deleted");
                }
            }
        }

        /// <summary>
        /// Deletes a game from the games list and writes it to the file
        /// </summary>
        private void DeleteGame()
        {
            games.Remove(game);
            Profile.GetInstance().SaveSettings(games);
            Profile.GetInstance().WriteProfile();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HelpPage));
        }

        /// <summary>
        /// Adds a new round to each of the players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRound_Click(object sender, RoutedEventArgs e)
        {
            if(EditMode)
            {
                //Add new player
                InputPlayerName();
            }
            else
            {
                for (int i = 0; i < players.Count; i++)
                {
                    GameRound gameRound = new GameRound() { RoundName = "Round " + (players[i].GameRounds.Count + 1), Score = 0, IsReadOnly = false };
                    players[i].GameRounds.Add(gameRound);
                    for (int j = 0; j < players[i].GameRounds.Count - 1; j++)
                    {
                        players[i].GameRounds[j].IsReadOnly = true;
                    }
                }
            }
        }

        private ObservableCollection<GameRound> CreateGameRounds()
        {
            ObservableCollection<GameRound> gameRounds = new ObservableCollection<GameRound>();
            for (int i = 0; i < players[0].GameRounds.Count; i++)
            {
                gameRounds.Add(new GameRound() { RoundName = "Round " + (i + 1), Score = 0, IsReadOnly = false });
            }
            return gameRounds;
        }

        private void ScoreTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            foreach (char c in args.NewText)
            {
                if (!char.IsDigit(c))
                {
                    args.Cancel = true;
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MyListView.SelectionMode = ListViewSelectionMode.Multiple;
            EditMode = true;
            DeleteButton.Label += " Player(s)";
            AddRound.Label = "Add player";
            EditButton.Visibility = Visibility.Collapsed;
            CancelEditButton.Visibility = Visibility.Visible;
        }

        private void CancelEditButton_Click(object sender, RoutedEventArgs e)
        {
            MyListView.SelectionMode = ListViewSelectionMode.None;
            EditMode = false;
            DeleteButton.Label = "Delete game";
            AddRound.Label = "Add round";
            EditButton.Visibility = Visibility.Visible;
            CancelEditButton.Visibility = Visibility.Collapsed;
        }

        private async void InputPlayerName()
        {
            ContentDialogResult result = await PlayerNameDialog.ShowAsync();
        }

        private void PlayerNameDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Player newPlayer = new Player() { PlayerName = PNameInput.Text, GameRounds = CreateGameRounds() };
            players.Add(newPlayer);
            for(int i = 0; i < newPlayer.GameRounds.Count - 1; i++)
            {
                newPlayer.GameRounds[i].IsReadOnly = true;
            }
        }

        private void ScoreTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            for(int i = 0; i < players.Count; i++)
            {
                int currScore = 0;
                for(int j = 0; j < players[i].GameRounds.Count; j++)
                {
                    currScore += players[i].GameRounds[j].Score;
                }
                players[i].TotalScore = currScore;
            }
        }
    }
}
