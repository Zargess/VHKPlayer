using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreatePlayableFile;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.CreatePlayableFilesFromFilesInFolder
{
    class CreatePlayablesFromFilesInFolderCommandHandler : ICommandHandler<CreatePlayableFilesFromFilesInFolderCommand>
    {
        private readonly ICommandProcessor processor;

        public CreatePlayablesFromFilesInFolderCommandHandler(ICommandProcessor processor)
        {
            this.processor = processor;
        }

        public void Handle(CreatePlayableFilesFromFilesInFolderCommand command)
        {
            foreach (var file in command.Folder.Content)
            {
                if (file.Type == FileType.Unsupported) continue;
                if (file.Type == FileType.Info) continue;

                processor.Process(new CreatePlayableFileCommand()
                {
                    File = file
                });
            }
        }
    }
}
