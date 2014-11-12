using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class FolderNode : IFolder {

        public ObservableCollection<IFile> Content { get; private set; }
        public string FullPath { get; private set; }
        public string Name { get; private set; }
        public string Source { get; private set; }

        public FolderNode(string path) {
            Name = Path.GetFileName(Path.GetDirectoryName(path));
        }

        public bool ContainsFolder(string name) {
            throw new NotImplementedException();
        }

        public bool ContainsFile(string path) {
            throw new NotImplementedException();
        }

        public bool ValidRootFolder() {
            throw new NotImplementedException();
        }

        public void InitWatcher() {
            throw new NotImplementedException();
        }

        public void StopWatcher() {
            throw new NotImplementedException();
        }
    }
}
