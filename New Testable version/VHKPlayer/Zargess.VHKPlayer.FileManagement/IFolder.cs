using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public interface IFolder : IWatchable {
        ObservableCollection<IFile> Content { get; }
        string FullPath { get; }
        string Name { get; }
        string Source { get; }
        bool ContainsFolder(string name);
        bool ContainsFile(string path);
        bool ValidRootFolder();
    }
}
