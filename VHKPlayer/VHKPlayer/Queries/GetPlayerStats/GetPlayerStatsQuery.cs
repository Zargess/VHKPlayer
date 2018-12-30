using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayerStats
{
    public class GetPlayerStatsQuery : IQuery<Statistics>
    {
        public Player Player { get; set; }
    }
}