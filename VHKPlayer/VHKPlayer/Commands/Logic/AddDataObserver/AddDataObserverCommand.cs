using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.AddDataObserver
{
    public class AddDataObserverCommand : ICommand
    {
        public IDataObserver Observer { get; set; }
    }
}