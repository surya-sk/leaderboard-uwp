using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard.Models
{
    public class GameRound
    {
        public string RoundName { get; set; }
        public int MaxScore { get; set; }
        public ObservableCollection<PlayerStat> PlayerStats { get; set; }

        public override string ToString()
        {
            return MaxScore + " " + PlayerStats;
        }
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
