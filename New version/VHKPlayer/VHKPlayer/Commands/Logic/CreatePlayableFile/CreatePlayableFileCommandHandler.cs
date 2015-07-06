using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.CreatePlayableFile
{
    class CreatePlayableFileCommandHandler : ICommandHandler<CreatePlayableFileCommand>
    {
        private readonly DataCenter center;

        public CreatePlayableFileCommandHandler(DataCenter center)
        {
            this.center = center;
        }

        public void Handle(CreatePlayableFileCommand command)
        {
            center.AddPlayableFile(new PlayableFile()
            {
                Name = command.File.Name,
                File = command.File
            });
        }
    }
}
