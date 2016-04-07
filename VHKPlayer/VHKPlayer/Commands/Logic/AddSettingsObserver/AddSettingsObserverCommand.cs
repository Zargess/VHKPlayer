using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.AddSettingsObserver
{
    public class AddSettingsObserverCommand : ICommand
    {
        public ISettingsObserver Observer { get; set; }
    }
}
