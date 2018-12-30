using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.IsValidRootFolder
{
    public class IsValidRootFolderQuery : IQuery<bool>
    {
        public string Path { get; set; }
    }
}