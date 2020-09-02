using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard.Models
{
    [System.Serializable]
    public class Game
    {
        public Guid Id { get; set; }
        public string GameName { get; set; }
        public string GameType { get; set; }
        public double NumPlayers { get; set; }
        public ObservableCollection<GameRound> GameRoundsList { get; set; }

        override
        public string ToString()
        {
            return Id+ " "+ GameName + " " + GameType + " " + NumPlayers + " " + GameRoundsList;
        }
    }

}
