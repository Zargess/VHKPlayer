using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayLists
{
    public class GetPlayListsQuery : IQuery<IQueryable<PlayList>>
    {
    }
}