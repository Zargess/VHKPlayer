using System.Linq;
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
            var folders = _queryProcessor.Process(new GetFoldersQuery()).ToList();
            var playableFiles = _queryProcessor.Process(new GetPlayableFilesQuery()).ToList();
            var playLists = _queryProcessor.Process(new GetPlayListsQuery()).ToList();
            var players = _queryProcessor.Process(new GetPlayersQuery()).ToList();

            for (var i = 0; i < folders.Count; i++)
            {
                _commandProcessor.Process(new RemoveFolderCommand()
                {
                    Folder = folders[i]
                });
            }

            for (var i = 0; i < playableFiles.Count; i++)
            {
                _commandProcessor.Process(new RemovePlayableFileCommand()
                {
                    PlayableFile = playableFiles[i]
                });
            }

            for (var i = 0; i < playLists.Count; i++)
            {
                _commandProcessor.Process(new RemovePlayListCommand()
                {
                    Playlist = playLists[i]
                });
            }

            for (var i = 0; i < players.Count; i++)
            {
                _commandProcessor.Process(new RemovePlayerCommand()
                {
                    Player = players[i]
                });
            }

            _commandProcessor.Process(new CreateAllPlayablesCommand());
        }
    }
}