using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard.Models
{
    public class GameRound
    {
        public int MaxScore { get; set; }
        public PlayerStatList PlayerStats { get; set; }

        public override string ToString()
        {
            return MaxScore + " " + PlayerStats;
        }
    }
}
