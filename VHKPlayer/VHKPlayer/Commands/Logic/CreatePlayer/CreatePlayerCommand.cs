using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.CreatePlayer
{
    public class CreatePlayerCommand : ICommand
    {
        public FileNode File { get; set; }
        public FolderNode Folder { get; set; }
    }
}