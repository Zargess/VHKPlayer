using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class FolderNode : Node{
        public List<FolderNode> SubFolders { get; private set; }
        public bool Exists { get; private set; }
        public List<FileNode> Files { get; private set; }
        private string _fullpath;
        public override string FullPath {
            get {
                return _fullpath;
            }
            set {
                _fullpath = value;
                var temp = PathHandler.SplitPath(_fullpath);
                Name = temp[temp.Length - 1];
                if (temp.Length > 1) {
                    Source = temp[temp.Length - 2];
                }
            }
        }

        public FolderNode(string path) {
            FullPath = path;
            Exists = Directory.Exists(FullPath);
            SubFolders = LoadSubFolders();
            Files = LoadFiles();
        }

        private List<FileNode> LoadFiles() {
            return Exists ? Directory.GetFiles(FullPath).Select(file => new FileNode(file)).ToList() : new List<FileNode>();
        }

        public List<FolderNode> LoadSubFolders() {
            if (!Exists) return new List<FolderNode>();
            var folders = Directory.GetDirectories(FullPath, "*", SearchOption.TopDirectoryOnly);
            return folders.Select(folder => new FolderNode(folder)).ToList();
        }

        public List<FolderNode> GetContent() {
            var res = new List<FolderNode> { new FolderNode(FullPath) };
            foreach (var folder in SubFolders) {
                res.AddRange(folder.GetContent().Select(f => new FolderNode(f.FullPath)));
            }
            return res;
        }

        public bool ContainsFile(string name) {
            return GetFile(name) != null;
        }

        public FileNode GetFile(string name) {
            return Files.SingleOrDefault(x => x.Name == name);
        }
    }
}