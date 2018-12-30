using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.AddApplicationObserver
{
    public class AddApplicationObserverCommand : ICommand
    {
        public IApplicationObserver Observer { get; set; }
    }
}