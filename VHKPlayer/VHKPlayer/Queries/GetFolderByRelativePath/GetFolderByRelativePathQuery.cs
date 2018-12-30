using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolderByRelativePath
{
    public class GetFolderByRelativePathQuery : IQuery<FolderNode>
    {
        public string RelativePath { get; set; }
    }
}