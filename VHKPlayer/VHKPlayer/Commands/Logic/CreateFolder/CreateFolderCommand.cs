using VHKPlayer.Commands.Logic.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateFolder
{
    public class CreateFolderCommand : ICommand
    {
        public string Path { get; set; }
    }
}