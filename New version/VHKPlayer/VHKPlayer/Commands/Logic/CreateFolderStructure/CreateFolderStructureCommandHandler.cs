using System.IO;
using System.Linq;
using VHKPlayer.Commands.Logic.CreateFolder;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Utility;

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
            var paths = Directory.EnumerateDirectories(command.RootFolderPath, "*", SearchOption.AllDirectories).ToList();

            var temp = App.Config.GetString(Constants.IgnoredFolderPath).Split(';');

            foreach (var path in temp)
            {
                paths.Remove(path.Replace("root", command.RootFolderPath));
            }

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