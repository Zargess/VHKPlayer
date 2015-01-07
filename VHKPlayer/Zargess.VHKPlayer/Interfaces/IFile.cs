using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IFile {
        string FullPath { get; }
        string Name { get; }
        string Source { get; }
        string NameWithoutExtension { get; }
        FileType Type { get; }
        bool Exists();
    }
}
