using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayers
{
    public class GetPlayersQuery : IQuery<IQueryable<Player>>
    {
    }
}