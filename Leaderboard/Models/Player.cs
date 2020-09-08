using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard.Models
{
    public class Player
    {
        public string PlayerName { get; set; }
        public ObservableCollection<GameRound> GameRounds { get; set; }
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
