using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemovePlayableFile
{
    class RemovePlayableFileCommandHandler : ICommandHandler<RemovePlayableFileCommand>
    {
        private readonly IDataCenter center;

        public RemovePlayableFileCommandHandler(IDataCenter center)
        {
            this.center = center;
        }

        public void Handle(RemovePlayableFileCommand command)
        {
            center.PlayableFiles.Remove(command.PlayableFile);
        }
    }
}
