using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreatePlayableFilesFromFilesInFolder;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Queries.GetPlayableFileFolders;
using VHKPlayer.Queries.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateAllPlayableFiles
{
    class CreateAllPlayableFilesCommandHandler : ICommandHandler<CreateAllPlayableFilesCommand>
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;

        public CreateAllPlayableFilesCommandHandler(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
        }

        public void Handle(CreateAllPlayableFilesCommand command)
        {
            var playableFileFolders = queryProcessor.Process(new GetPlayableFileFoldersQuery());
            if (playableFileFolders.Count() == 0) return;
            foreach (var folder in playableFileFolders)
            {
                commandProcessor.Process(new CreatePlayableFilesFromFilesInFolderCommand()
                {
                    Folder = folder
                });
            }
        }
    }
}
