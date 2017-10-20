﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.AddApplicationObserver
{
    public class AddApplicationObserverCommand : ICommand
    {
        public IApplicationObserver Observer { get; set; }
    }
}