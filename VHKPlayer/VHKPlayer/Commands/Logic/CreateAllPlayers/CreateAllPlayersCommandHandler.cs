using VHKPlayer.Commands.Logic.CreatePlayer;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderFromStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Commands.Logic.CreateAllPlayers
{
    class CreateAllPlayersCommandHandler : ICommandHandler<CreateAllPlayersCommand>
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public CreateAllPlayersCommandHandler(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this._commandProcessor = commandProcessor;
            this._queryProcessor = queryProcessor;
        }

        public void Handle(CreateAllPlayersCommand command)
        {
            var folder = _queryProcessor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerPictureFolderSettingName
            });

            var statFolder = _queryProcessor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerStatisticInformation
            });

            if (folder == null) return;

            foreach (var file in folder.Content)
            {
                if (file.Type != FileType.Picture) continue;

                _commandProcessor.Process(new CreatePlayerCommand()
                {
                    File = file,
                    Folder = statFolder
                });
            }
        }
    }
}