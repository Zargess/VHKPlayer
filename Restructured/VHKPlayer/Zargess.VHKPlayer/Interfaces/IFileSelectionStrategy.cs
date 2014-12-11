﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IFileSelectionStrategy {
        Queue<IFile> SelectFiles(IPlayable playable);
    }
}