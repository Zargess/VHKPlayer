﻿using ScriptParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Models.Interfaces
{
    public interface IScript
    {
        Program Code { get; }
    }
}