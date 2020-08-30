using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaderboard.Models;

namespace Leaderboard
{
    class UpdateEvent
    {
        IGamesUpdateable MainPage;
        public UpdateEvent(IGamesUpdateable mainPage)
        {
            this.MainPage = mainPage;
        }

        public void OnEventChanged(ObservableCollection<Game> games)
        {
            this.MainPage.UpdateGames(games);
        }

    }
}
