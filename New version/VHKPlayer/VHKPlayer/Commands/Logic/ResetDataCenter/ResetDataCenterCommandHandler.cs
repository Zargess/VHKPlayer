using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreateAllPlayables;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Commands.Logic.RemoveFolder;
using VHKPlayer.Commands.Logic.RemovePlayableFile;
using VHKPlayer.Commands.Logic.RemovePlayer;
using VHKPlayer.Commands.Logic.RemovePlayList;
using VHKPlayer.Queries.GetFolders;
using VHKPlayer.Queries.GetPlayableFiles;
using VHKPlayer.Queries.GetPlayers;
using VHKPlayer.Queries.GetPlayLists;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Commands.Logic.ResetDataCenter
{
    class ResetDataCenterCommandHandler : ICommandHandler<ResetDataCenterCommand>
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;

        public ResetDataCenterCommandHandler(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
        }

        public void Handle(ResetDataCenterCommand command)
        {
            var folders = queryProcessor.Process(new GetFoldersQuery());
            var playableFiles = queryProcessor.Process(new GetPlayableFilesQuery());
            var playLists = queryProcessor.Process(new GetPlayListsQuery());
            var players = queryProcessor.Process(new GetPlayersQuery());

            foreach (var folder in folders)
            {
                commandProcessor.Process(new RemoveFolderCommand()
                {
                    Folder = folder
                });
            }

            foreach (var playableFile in playableFiles)
            {
                commandProcessor.Process(new RemovePlayableFileCommand()
                {
                    PlayableFile = playableFile
                });
            }

            foreach (var playlist in playLists)
            {
                commandProcessor.Process(new RemovePlayListCommand()
                {
                    Playlist = playlist
                });
            }

            foreach (var player in players)
            {
                commandProcessor.Process(new RemovePlayerCommand()
                {
                    Player = player
                });
            }

            commandProcessor.Process(new CreateAllPlayablesCommand());
        }
    }
}
