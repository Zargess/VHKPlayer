﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RegisterTimer
{
    class RegisterTimerCommandHandler : ICommandHandler<RegisterTimerCommand>
    {
        private readonly IDataCenter center;

        public RegisterTimerCommandHandler(IDataCenter center)
        {
            this.center = center;
        }

        public void Handle(RegisterTimerCommand command)
        {
            center.Timers.Add(command.Timer);
        }
    }
}
