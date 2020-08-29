using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard.Models
{
    [System.Serializable]
    class Game
    {
        public int id { get; set; }
        public string GameName { get; set; }
        public string GameType { get; set; }
        public double NumPlayers { get; set; }
        public ObservableCollection<PlayerStat> PlayerStatList { get; set; }

        override
        public string ToString()
        {
            return GameName + " " + GameType + " " + NumPlayers + " " + PlayerStatList;
        }
    }

}
