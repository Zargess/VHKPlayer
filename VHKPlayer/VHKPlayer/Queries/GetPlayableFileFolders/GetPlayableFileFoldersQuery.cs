using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayableFileFolders
{
    public class GetPlayableFileFoldersQuery : IQuery<IQueryable<FolderNode>>
    {
    }
}