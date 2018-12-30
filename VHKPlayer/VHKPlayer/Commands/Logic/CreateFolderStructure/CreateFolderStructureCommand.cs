using VHKPlayer.Commands.Logic.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateFolderStructure
{
    public class CreateFolderStructureCommand : ICommand
    {
        public string RootFolderPath { get; set; }
    }
}