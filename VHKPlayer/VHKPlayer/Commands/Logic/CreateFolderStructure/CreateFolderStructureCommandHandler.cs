using System.IO;
using System.Linq;
using VHKPlayer.Commands.Logic.CreateFolder;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Commands.Logic.CreateFolderStructure
{
    class CreateFolderStructureCommandHandler : ICommandHandler<CreateFolderStructureCommand>
    {
        private readonly IDataCenter _center;
        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public CreateFolderStructureCommandHandler(IDataCenter center, ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this._center = center;
            this._commandProcessor = commandProcessor;
            this._queryProcessor = queryProcessor;
        }

        // TODO : Consider using GetRootFolderQuery instead of giveng the root folder to the command
        public void Handle(CreateFolderStructureCommand command)
        {
            if (!Directory.Exists(command.RootFolderPath)) return;

            var paths = Directory.EnumerateDirectories(command.RootFolderPath, "*", SearchOption.AllDirectories).ToList();

            var temp = _queryProcessor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.IgnoredFolderPathSettingName
            }).Split(';');

            foreach (var path in temp)
            {
                paths.Remove(path.Replace("root", command.RootFolderPath));
            }

            _commandProcessor.Process(new CreateFolderCommand()
            {
                Path = command.RootFolderPath
            });

            foreach (var path in paths)
            {
                _commandProcessor.Process(new CreateFolderCommand()
                {
                    Path = path
                });
            }

            var statFolder = _queryProcessor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.PlayerStatisticInformation
            });

            _commandProcessor.Process(new CreateFolderCommand()
            {
                Path = statFolder
            });
        }
    }
}