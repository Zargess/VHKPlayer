using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayableFiles
{
    public class GetPlayableFilesQuery : IQuery<IQueryable<PlayableFile>>
    {
    }
}