using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Utility.IsValidRootFolder.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateFolder
{
    class CreateFolderCommandHandler : ICommandHandler<CreateFolderCommand>
    {
        private readonly DataCenter center;
        private readonly ICommandProcessor processor;
        private readonly IValidRootFolderStrategy strategy;

        public CreateFolderCommandHandler(DataCenter center, ICommandProcessor processor, IValidRootFolderStrategy strategy)
        {
            this.center = center;
            this.processor = processor;
            this.strategy = strategy;
        }

        public void Handle(CreateFolderCommand command)
        {
            center.AddFolder(new FolderNode(processor)
            {
                FullPath = command.Path,
                ValidRootFolder = strategy
            });
        }
    }
}
