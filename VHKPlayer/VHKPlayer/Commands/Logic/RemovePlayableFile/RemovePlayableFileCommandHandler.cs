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
        private readonly IDataCenter _center;

        public RemovePlayableFileCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        public void Handle(RemovePlayableFileCommand command)
        {
            _center.PlayableFiles.Remove(command.PlayableFile);
        }
    }
}
