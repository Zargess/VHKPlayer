using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayerStats
{
    public class GetPlayerStatsQuery : IQuery<Statistics>
    {
        public Player Player { get; set; }
    }
}
