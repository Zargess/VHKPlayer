﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;

namespace VHKPlayer.Interfaces {
    public interface IPlayStrategy {
        void Play(IVideoPlayer videoplayer, IFile file, PlayType type);
    }
}
