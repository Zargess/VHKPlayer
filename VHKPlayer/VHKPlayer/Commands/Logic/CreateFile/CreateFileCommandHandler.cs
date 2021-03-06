﻿using System.IO;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Utility.FindFileType.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateFile
{
    class CreateFileCommandHandler : ICommandHandler<CreateFileCommand>
    {
        private readonly IFindFileTypeStrategy _findFileType;

        public CreateFileCommandHandler(IFindFileTypeStrategy findFileType)
        {
            this._findFileType = findFileType;
        }

        public void Handle(CreateFileCommand command)
        {
            var type = _findFileType.FindType(command.Path);
            var name = Path.GetFileName(command.Path);
            var namewithoutextension = Path.GetFileNameWithoutExtension(command.Path);

            command.Folder.AddFile(new FileNode()
            {
                FullPath = command.Path,
                Name = name,
                NameWithoutExtension = namewithoutextension,
                Type = type
            });
        }
    }
}