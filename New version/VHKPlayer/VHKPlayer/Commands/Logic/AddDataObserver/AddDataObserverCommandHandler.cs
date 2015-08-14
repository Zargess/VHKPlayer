using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.AddDataObserver
{
    class AddDataObserverCommandHandler : ICommandHandler<AddDataObserverCommand>
    {
        private readonly IDataCenter center;

        public AddDataObserverCommandHandler(IDataCenter center)
        {
            this.center = center;
        }

        public void Handle(AddDataObserverCommand command)
        {
            this.center.AddObserver(command.Observer);
        }
    }
}
