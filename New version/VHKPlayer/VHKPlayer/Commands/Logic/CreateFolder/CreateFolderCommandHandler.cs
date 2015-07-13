using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.CreateFolder
{
    class CreateFolderCommandHandler : ICommandHandler<CreateFolderCommand>
    {
        private readonly DataCenter center;
        private readonly ICommandProcessor processor;

        public CreateFolderCommandHandler(DataCenter center, ICommandProcessor processor)
        {
            this.center = center;
            this.processor = processor;
        }

        public void Handle(CreateFolderCommand command)
        {
            center.Folders.Add(new FolderNode(processor)
            {
                FullPath = command.Path
            });
        }
    }
}
