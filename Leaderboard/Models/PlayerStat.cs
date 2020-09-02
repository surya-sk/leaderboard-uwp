using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaderboard.Models
{
    public class PlayerStat
    {
        public string PlayerName { get; set; }
        public double PlayerScore { get; set; }
    }
    [System.Serializable]
    public class PlayerStatList
    {
        public static ObservableCollection<PlayerStat> GetPlayerStats()
        {
            var PlayerSt = new ObservableCollection<PlayerStat>();
            return PlayerSt;
        }

    }
}
