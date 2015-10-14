using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreateAllPlayers;
using VHKPlayer.Commands.Logic.CreatePlayableFilesFromFilesInFolder;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Commands.Logic.RemovePlayableFile;
using VHKPlayer.Commands.Logic.RemovePlayer;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetPlayablesAffectedByFolder;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Commands.Logic.UpdateDataCenterByFolder
{
    class UpdateDataCenterByFolderCommandHandler : ICommandHandler<UpdateDataCenterByFolderCommand>
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;

        public UpdateDataCenterByFolderCommandHandler(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
        }

        public void Handle(UpdateDataCenterByFolderCommand command)
        {
            var playables = queryProcessor.Process(new GetPlayablesAffectedByFolderQuery()
            {
                Folder = command.Folder
            });

            var players = playables.Where(x => x is Player).Select(x => x as Player);

            if (players.Count() > 0)
            {
                foreach (var player in players)
                {
                    commandProcessor.Process(new RemovePlayerCommand()
                    {
                        Player = player
                    });
                }

                commandProcessor.Process(new CreateAllPlayersCommand());
            }

            var playableFiles = playables.Where(x => x is PlayableFile).Select(x => x as PlayableFile).ToList();

            if (playableFiles.Count() > 0)
            {
                foreach (var playablefile in playableFiles)
                {
                    commandProcessor.Process(new RemovePlayableFileCommand()
                    {
                        PlayableFile = playablefile
                    });
                }

                commandProcessor.Process(new CreatePlayableFilesFromFilesInFolderCommand()
                {
                    Folder = command.Folder
                });
            }
        }
    }
}
