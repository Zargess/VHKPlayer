using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement.Interfaces {
    public interface IFile {
        string FullPath { get; }
        string Name { get; }
        string NameWithoutExtension { get; }
        string Source { get; }
        FileType Type { get; }
        bool Exists();
        IFile Clone();
    }
}
