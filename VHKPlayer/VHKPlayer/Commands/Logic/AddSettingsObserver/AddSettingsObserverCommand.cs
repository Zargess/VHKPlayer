using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.AddSettingsObserver
{
    public class AddSettingsObserverCommand : ICommand
    {
        public ISettingsObserver Observer { get; set; }
    }
}