using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Monitors.Interfaces;

namespace VHKPlayer.Commands.Logic.AddApplicationObserver
{
    class AddApplicationObserverCommandHandler : ICommandHandler<AddApplicationObserverCommand>
    {
        private readonly IApplicationMonitor _monitor;

        public AddApplicationObserverCommandHandler(IApplicationMonitor monitor)
        {
            _monitor = monitor;
        }

        public void Handle(AddApplicationObserverCommand command)
        {
            _monitor.AddObserver(command.Observer);
        }
    }
}
