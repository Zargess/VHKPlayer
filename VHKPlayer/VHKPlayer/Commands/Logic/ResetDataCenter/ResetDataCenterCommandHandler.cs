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
        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public ResetDataCenterCommandHandler(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this._commandProcessor = commandProcessor;
            this._queryProcessor = queryProcessor;
        }

        public void Handle(ResetDataCenterCommand command)
        {
            var folders = _queryProcessor.Process(new GetFoldersQuery());
            var playableFiles = _queryProcessor.Process(new GetPlayableFilesQuery());
            var playLists = _queryProcessor.Process(new GetPlayListsQuery());
            var players = _queryProcessor.Process(new GetPlayersQuery());

            foreach (var folder in folders)
            {
                _commandProcessor.Process(new RemoveFolderCommand()
                {
                    Folder = folder
                });
            }

            foreach (var playableFile in playableFiles)
            {
                _commandProcessor.Process(new RemovePlayableFileCommand()
                {
                    PlayableFile = playableFile
                });
            }

            foreach (var playlist in playLists)
            {
                _commandProcessor.Process(new RemovePlayListCommand()
                {
                    Playlist = playlist
                });
            }

            foreach (var player in players)
            {
                _commandProcessor.Process(new RemovePlayerCommand()
                {
                    Player = player
                });
            }

            _commandProcessor.Process(new CreateAllPlayablesCommand());
        }
    }
}
