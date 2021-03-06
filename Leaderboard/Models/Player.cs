﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard.Models
{
    public class Player : INotifyPropertyChanged
    {
        public string PlayerName { get; set; }
        public ObservableCollection<GameRound> GameRounds { get; set; }
        private int totalScore;
        private string playerColor;

        public event PropertyChangedEventHandler PropertyChanged;

        public int TotalScore
        { get => totalScore;
            set
            {
                totalScore = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalScore)));
            }
        }

        public string PlayerColor { get => playerColor;
            set
            {
                playerColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlayerColor)));
            }
        }
    }

    public class PlayerList
    {
        public static ObservableCollection<Player> GetPlayers()
        {
            var PlayerList = new ObservableCollection<Player>();
            return PlayerList;
        }
    }

}
