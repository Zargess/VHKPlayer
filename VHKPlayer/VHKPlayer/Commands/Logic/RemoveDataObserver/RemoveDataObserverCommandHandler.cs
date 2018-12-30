using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemoveDataObserver
{
    class RemoveDataObserverCommandHandler : ICommandHandler<RemoveDataObserverCommand>
    {
        private readonly IDataCenter _center;

        public RemoveDataObserverCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        public void Handle(RemoveDataObserverCommand command)
        {
            this._center.RemoveObserver(command.Observer);
        }
    }
}