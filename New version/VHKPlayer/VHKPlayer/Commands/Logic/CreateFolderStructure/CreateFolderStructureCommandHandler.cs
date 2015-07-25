using System.IO;
using System.Linq;
using VHKPlayer.Commands.Logic.CreateFolder;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Commands.Logic.CreateFolderStructure
{
    class CreateFolderStructureCommandHandler : ICommandHandler<CreateFolderStructureCommand>
    {
        private readonly DataCenter center;
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;

        public CreateFolderStructureCommandHandler(DataCenter center, ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this.center = center;
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
        }

        // TODO : Consider using GetRootFolderQuery instead of giveng the root folder to the command
        public void Handle(CreateFolderStructureCommand command)
        {
            var paths = Directory.EnumerateDirectories(command.RootFolderPath, "*", SearchOption.AllDirectories).ToList();

            var temp = queryProcessor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.IgnoredFolderPathSettingName
            }).Split(';');

            foreach (var path in temp)
            {
                paths.Remove(path.Replace("root", command.RootFolderPath));
            }

            commandProcessor.Process(new CreateFolderCommand()
            {
                Path = command.RootFolderPath
            });

            foreach (var path in paths)
            {
                commandProcessor.Process(new CreateFolderCommand()
                {
                    Path = command.RootFolderPath
                });
            }

            commandProcessor.Process(new CreateFolderCommand()
            {
                Path = queryProcessor.Process(new GetStringSettingQuery()
                {
                    SettingName = Constants.PlayerStatisticInformation
                })
            });
        }
    }
}