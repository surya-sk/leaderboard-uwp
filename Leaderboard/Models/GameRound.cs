﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard.Models
{
    /// <summary>
    /// A model for each GameRound that is an object of a Player object
    /// </summary>
    public class GameRound
    {
        public string RoundName { get; set; }
        public int score { get; set; }
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