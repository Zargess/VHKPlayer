using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Queries.GetFolderFromStringSetting
{
    public class GetFolderFromStringSettingQuery : IQuery<FolderNode>
    {
        public string SettingName { get; set; }
    }
}