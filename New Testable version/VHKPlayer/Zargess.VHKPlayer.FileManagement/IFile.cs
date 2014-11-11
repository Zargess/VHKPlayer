﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public interface IFile {
        string FullPath { get; }
        string Name { get; }
        string Source { get; }
        FileType Type { get; }
        bool Exists();
    }
}
