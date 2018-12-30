using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.RemoveFolder
{
    public class RemoveFolderCommand : ICommand
    {
        public FolderNode Folder { get; set; }
    }
}