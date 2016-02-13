using VHKPlayer.Commands.Logic.CreateAllPlayableFiles;
using VHKPlayer.Commands.Logic.CreateAllPlayers;
using VHKPlayer.Commands.Logic.CreateAllPlayLists;
using VHKPlayer.Commands.Logic.CreateFolderStructure;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Commands.Logic.CreateAllPlayables
{
    class CreateAllPlayablesCommandHandler : ICommandHandler<CreateAllPlayablesCommand>
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public CreateAllPlayablesCommandHandler(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this._commandProcessor = commandProcessor;
            this._queryProcessor = queryProcessor;
        }

        public void Handle(CreateAllPlayablesCommand command)
        {
            var root = Constants.RootFolderPathSettingName;

            if (string.IsNullOrEmpty(root)) return;

            _commandProcessor.Process(new CreateFolderStructureCommand()
            {
                RootFolderPath = _queryProcessor.Process(new GetStringSettingQuery()
                {
                    SettingName = root
                })
            });

            _commandProcessor.Process(new CreateAllPlayableFilesCommand());
            
            _commandProcessor.Process(new CreateAllPlayListsCommand());

            _commandProcessor.Process(new CreateAllPlayersCommand());
        }
    }
}