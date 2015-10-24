using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.CreatePlayableFile
{
    class CreatePlayableFileCommandHandler : ICommandHandler<CreatePlayableFileCommand>
    {
        private readonly IDataCenter center;

        public CreatePlayableFileCommandHandler(IDataCenter center)
        {
            this.center = center;
        }

        public void Handle(CreatePlayableFileCommand command)
        {
            center.PlayableFiles.Add(new PlayableFile()
            {
                Name = command.File.Name,
                File = command.File
            });
        }
    }
}
