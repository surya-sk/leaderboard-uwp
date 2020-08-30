using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaderboard.Models;

namespace Leaderboard
{
    interface IGamesUpdateable
    {
        void UpdateGames(ObservableCollection<Game> games);
    }
}
