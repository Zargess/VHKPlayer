using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Monitors.Interfaces;

namespace VHKPlayer.Commands.Logic.AddSettingsObserver
{
    class AddSettingsObserverCommandHandler : ICommandHandler<AddSettingsObserverCommand>
    {
        private readonly ISettingsMonitor _monitor;

        public AddSettingsObserverCommandHandler(ISettingsMonitor monitor)
        {
            _monitor = monitor;
        }

        public void Handle(AddSettingsObserverCommand command)
        {
            _monitor.AddObserver(command.Observer);
        }
    }
}
