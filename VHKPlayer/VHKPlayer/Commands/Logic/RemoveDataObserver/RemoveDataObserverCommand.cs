using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemoveDataObserver
{
    public class RemoveDataObserverCommand : ICommand
    {
        public IDataObserver Observer { get; set; }
    }
}