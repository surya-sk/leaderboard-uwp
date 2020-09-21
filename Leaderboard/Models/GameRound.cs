using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard.Models
{
    /// <summary>
    /// A model for each GameRound that is an object of a Player object
    /// </summary>
    public class GameRound : INotifyPropertyChanged
    {
        private string roundName;
        private int score;
        private bool isReadOnly;
        public int Score
        { get => score;
            set
            {
                score = value;
                Debug.WriteLine("Test");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Score)));
            }
        }
        public bool IsReadOnly
        { get => isReadOnly;
            set
            {
                isReadOnly = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsReadOnly)));
            }
        }

        public string RoundName
        { get => roundName;
            set
            {
                roundName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RoundName)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class GameRoundsList
    {
        public static ObservableCollection<GameRound> GetGameRounds()
        {
            var GameRounds = new ObservableCollection<GameRound>();
            return GameRounds;
        }
    }
}
