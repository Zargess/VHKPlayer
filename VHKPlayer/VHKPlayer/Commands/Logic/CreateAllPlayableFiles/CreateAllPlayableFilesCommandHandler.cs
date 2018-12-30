using System.Linq;
using VHKPlayer.Commands.Logic.CreatePlayableFilesFromFilesInFolder;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Queries.GetPlayableFileFolders;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateAllPlayableFiles
{
    class CreateAllPlayableFilesCommandHandler : ICommandHandler<CreateAllPlayableFilesCommand>
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public CreateAllPlayableFilesCommandHandler(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this._commandProcessor = commandProcessor;
            this._queryProcessor = queryProcessor;
        }

        public void Handle(CreateAllPlayableFilesCommand command)
        {
            var playableFileFolders = _queryProcessor.Process(new GetPlayableFileFoldersQuery());
            if (playableFileFolders.Count() == 0) return;
            foreach (var folder in playableFileFolders)
            {
                _commandProcessor.Process(new CreatePlayableFilesFromFilesInFolderCommand()
                {
                    Folder = folder
                });
            }
        }
    }
}