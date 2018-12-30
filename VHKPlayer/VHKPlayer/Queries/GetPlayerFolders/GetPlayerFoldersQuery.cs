using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetPlayerFolders
{
    public class GetPlayerFoldersQuery : IQuery<IQueryable<FolderNode>>
    {
    }
}