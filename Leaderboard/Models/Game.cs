using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard.Models
{
    /// <summary>
    /// A modal of the each game added
    /// </summary>
    [System.Serializable]
    public class Game
    {
        //instance variables
        public Guid Id { get; set; }
        public string GameName { get; set; }
        public string GameType { get; set; }
        public double NumPlayers { get; set; }
        public int MaxScore { get; set; }
        public ObservableCollection<Player> Players { get; set; }
    }

}
