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
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;

        public CreateAllPlayablesCommandHandler(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
        }

        public void Handle(CreateAllPlayablesCommand command)
        {
            commandProcessor.Process(new CreateFolderStructureCommand()
            {
                RootFolderPath = queryProcessor.Process(new GetStringSettingQuery()
                {
                    SettingName = Constants.RootFolderPathSettingName
                })
            });

            commandProcessor.Process(new CreateAllPlayableFilesCommand());

            // TODO : Make a way to create playlists
            commandProcessor.Process(new CreateAllPlayListsCommand());

            commandProcessor.Process(new CreateAllPlayersCommand());
        }
    }
}