using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolders
{
    public class GetFoldersQuery : IQuery<IQueryable<FolderNode>>
    {
    }
}