using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreatePlayer;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderByPathSubscript;
using VHKPlayer.Queries.GetFolderFromStringSetting;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Commands.Logic.CreateAllPlayers
{
    class CreateAllPlayersCommandHandler : ICommandHandler<CreateAllPlayersCommand>
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;

        public CreateAllPlayersCommandHandler(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
        }

        public void Handle(CreateAllPlayersCommand command)
        {
            var folder = queryProcessor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerPictureFolderSettingName
            });

            var statFolder = queryProcessor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerStatisticInformation
            });

            foreach (var file in folder.Content)
            {
                if (file.Type != FileType.Picture) continue;

                commandProcessor.Process(new CreatePlayerCommand()
                {
                    File = file,
                    Folder = statFolder
                });
            }
        }
    }
}
