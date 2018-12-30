using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolderByPathSubscript
{
    public class GetFolderByPathSubscriptQuery : IQuery<FolderNode>
    {
        public string PartialPath { get; set; }
    }
}