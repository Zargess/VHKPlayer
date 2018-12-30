using VHKPlayer.Commands.Logic.CreatePlayableFile;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.CreatePlayableFilesFromFilesInFolder
{
    class CreatePlayablesFromFilesInFolderCommandHandler : ICommandHandler<CreatePlayableFilesFromFilesInFolderCommand>
    {
        private readonly ICommandProcessor _processor;

        public CreatePlayablesFromFilesInFolderCommandHandler(ICommandProcessor processor)
        {
            this._processor = processor;
        }

        public void Handle(CreatePlayableFilesFromFilesInFolderCommand command)
        {
            foreach (var file in command.Folder.Content)
            {
                if (file.Type == FileType.Unsupported) continue;
                if (file.Type == FileType.Info) continue;

                _processor.Process(new CreatePlayableFileCommand()
                {
                    File = file
                });
            }
        }
    }
}