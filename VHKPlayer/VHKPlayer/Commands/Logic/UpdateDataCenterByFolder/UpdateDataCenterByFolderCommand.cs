using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.UpdateDataCenterByFolder
{
    public class UpdateDataCenterByFolderCommand : ICommand
    {
        public FolderNode Folder { get; set; }
    }
}