using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Utility.FindFileType.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateFile
{
    class CreateFileCommandHandler : ICommandHandler<CreateFileCommand>
    {
        private readonly IFindFileTypeStrategy findFileType;

        public CreateFileCommandHandler(IFindFileTypeStrategy findFileType)
        {
            this.findFileType = findFileType;
        }

        public void Handle(CreateFileCommand command)
        {
            var type = findFileType.FindType(command.Path);
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
