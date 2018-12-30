using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Monitors.Interfaces;

namespace VHKPlayer.Commands.Logic.RemoveApplicationObserver
{
    class RemoveApplicationObserverCommandHandler : ICommandHandler<RemoveApplicationObserverCommand>
    {
        private readonly IApplicationMonitor _monitor;

        public RemoveApplicationObserverCommandHandler(IApplicationMonitor monitor)
        {
            _monitor = monitor;
        }

        public void Handle(RemoveApplicationObserverCommand command)
        {
            _monitor.RemoveObserver(command.Observer);
        }
    }
}