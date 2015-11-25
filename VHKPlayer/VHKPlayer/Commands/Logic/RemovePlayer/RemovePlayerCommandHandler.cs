using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemovePlayer
{
    class RemovePlayerCommandHandler : ICommandHandler<RemovePlayerCommand>
    {
        private readonly IDataCenter _center;

        public RemovePlayerCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        public void Handle(RemovePlayerCommand command)
        {
            _center.Players.Remove(command.Player);
        }
    }
}
