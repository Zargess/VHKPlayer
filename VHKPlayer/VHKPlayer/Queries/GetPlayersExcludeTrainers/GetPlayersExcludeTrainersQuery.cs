using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayersExcludeTrainers
{
    public class GetPlayersExcludeTrainersQuery : IQuery<IQueryable<Player>>
    {
    }
}