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
        public event EventHandler FolderChanged;

        public FolderNode(string path) {
            Name = Path.GetFileName(path);
            FullPath = GetFilePath(path);
            Source = GetSource();
            Content = GetFiles();
        }

        private ObservableCollection<IFile> GetFiles() {
            var res = new ObservableCollection<IFile>();
            try {
                var paths = Directory.EnumerateFiles(FullPath);
                foreach (var path in paths) {
                    res.Add(new FileNode(path));
                }
            } catch (DirectoryNotFoundException) {}
            return res;
        }

        private string GetSource() {
            var temp = FullPath.Split('\\');
            return String.IsNullOrEmpty(temp[temp.Length - 1]) ? temp[temp.Length - 3] : temp[temp.Length - 2];
        }

        private string GetFilePath(string path) {
            var temp = path.Split('\\');
            return temp.Length > 1 ? path : Path.Combine(Environment.CurrentDirectory, path);
        }

        public bool ContainsFolder(IFolder folder) {
            var folders = Directory.EnumerateDirectories(FullPath).Select(x => new FolderNode(x));
            return folders.Any(f => String.Equals(f.FullPath, folder.FullPath, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool ContainsFile(IFile file) {
            return Content.Any(f => String.Equals(f.FullPath, file.FullPath, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool ValidRootFolder() {
            throw new NotImplementedException();
        }

        public bool InitWatcher() {
            throw new NotImplementedException();
        }

        public bool StopWatcher() {
            throw new NotImplementedException();
        }
    }
}
