using System.IO;
using VHKPlayer.Commands.Logic.CreateFolder;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.CreateFolderStructure
{
    class CreateFolderStructureCommandHandler : ICommandHandler<CreateFolderStructureCommand>
    {
        private readonly DataCenter center;
        private readonly ICommandProcessor processor;

        public CreateFolderStructureCommandHandler(DataCenter center, ICommandProcessor processor)
        {
            this.center = center;
            this.processor = processor;
        }

        public void Handle(CreateFolderStructureCommand command)
        {
            var paths = Directory.EnumerateDirectories(command.RootFolderPath, "*", SearchOption.AllDirectories);
            processor.Process(new CreateFolderCommand()
            {
                Path = command.RootFolderPath
            });

            foreach (var path in paths)
            {
                processor.Process(new CreateFolderCommand()
                {
                    Path = command.RootFolderPath
                });
            }
        }
    }
}