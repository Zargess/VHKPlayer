using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.AddDataObserver
{
    class AddDataObserverCommandHandler : ICommandHandler<AddDataObserverCommand>
    {
        private readonly IDataCenter _center;

        public AddDataObserverCommandHandler(IDataCenter center)
        {
            this._center = center;
        }

        public void Handle(AddDataObserverCommand command)
        {
            this._center.AddObserver(command.Observer);
        }
    }
}