using System.Linq;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateFolder
{
    class CreateFolderCommandHandler : ICommandHandler<CreateFolderCommand>
    {
        private readonly ICommandProcessor processor;
        private readonly IDataCenter center;

        public CreateFolderCommandHandler(IDataCenter center, ICommandProcessor processor)
        {
            this.center = center;
            this.processor = processor;
        }

        public void Handle(CreateFolderCommand command)
        {
            if (center.Folders.Any(x => x.FullPath.ToLower() == command.Path.ToLower())) return;
            center.Folders.Add(new FolderNode(processor)
            {
                FullPath = command.Path
            });
        }
    }
}
