using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemoveDataObserver
{
    class RemoveDataObserverCommandHandler : ICommandHandler<RemoveDataObserverCommand>
    {
        private readonly IDataCenter center;

        public RemoveDataObserverCommandHandler(IDataCenter center)
        {
            this.center = center;
        }

        public void Handle(RemoveDataObserverCommand command)
        {
            this.center.RemoveObserver(command.Observer);
        }
    }
}
