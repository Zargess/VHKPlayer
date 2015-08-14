﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Commands.Logic.RemoveDataObserver
{
    public class RemoveDataObserverCommand : ICommand
    {
        public IDataObserver Observer { get; set; }
    }
}