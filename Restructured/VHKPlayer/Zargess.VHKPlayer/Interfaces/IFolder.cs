using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IFolder : IWatchable {
        List<IFile> Content { get; }
        string FullPath { get; }
        string Name { get; }
        event EventHandler FolderChanged;

        bool Exists();
        bool ContainsFolder(IFolder folder);
        bool ContainsFile(IFile file);
        bool ValidRootFolder();
        IFolder Clone();
    }
}
