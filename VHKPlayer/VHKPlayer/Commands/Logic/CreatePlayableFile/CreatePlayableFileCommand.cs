using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.CreatePlayableFile
{
    public class CreatePlayableFileCommand : ICommand
    {
        public FileNode File { get; set; }
    }
}