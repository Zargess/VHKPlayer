﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.RemovePlayableFile
{
    public class RemovePlayableFileCommand : ICommand
    {
        public PlayableFile PlayableFile { get; set; }
    }
}