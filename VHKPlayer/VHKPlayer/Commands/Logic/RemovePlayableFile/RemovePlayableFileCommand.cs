using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.RemovePlayableFile
{
    public class RemovePlayableFileCommand : ICommand
    {
        public PlayableFile PlayableFile { get; set; }
    }
}