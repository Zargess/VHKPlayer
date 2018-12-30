using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemoveApplicationObserver
{
    public class RemoveApplicationObserverCommand : ICommand
    {
        public IApplicationObserver Observer { get; set; }
    }
}