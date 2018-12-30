using System.Linq;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetAllPlayables
{
    public class GetAllPlayablesQuery : IQuery<IQueryable<IPlayable>>
    {
    }
}