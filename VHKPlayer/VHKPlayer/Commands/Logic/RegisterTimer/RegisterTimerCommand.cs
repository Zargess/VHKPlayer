using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using VHKPlayer.Commands.Logic.Interfaces;

namespace VHKPlayer.Commands.Logic.RegisterTimer
{
    public class RegisterTimerCommand : ICommand
    {
        public Timer Timer { get; set; }
    }
}
