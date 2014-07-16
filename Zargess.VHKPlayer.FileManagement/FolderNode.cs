using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class FolderNode : Node{
        public List<FolderNode> SubFolders { get; private set; }
        public bool Exists { get; private set; }
        public ObservableCollection<FileNode> Files { get; private set; }
        private string _fullpath;
        public override sealed string FullPath {
            get {
                return _fullpath;
            }
            set {
                if (String.IsNullOrEmpty(value))  return;
                _fullpath = value;
                var temp = PathHandler.SplitPath(_fullpath);
                Name = temp[temp.Length - 1];
                if (temp.Length > 1) Source = temp[temp.Length - 2];
            }
        }

        public FolderNode(string path) {
            FullPath = path;
            Exists = Directory.Exists(FullPath);
            SubFolders = LoadSubFolders();
            Files = LoadFiles();
        }

        private ObservableCollection<FileNode> LoadFiles() {
            if (!Exists)  return new ObservableCollection<FileNode>();
            var temp = Directory.GetFiles(FullPath)
                    .Select(x => new FileNode(x))
                    .Where(x => x.Type != FileType.Unsupported);
            var res = new ObservableCollection<FileNode>();
            foreach (var fileNode in temp) {
                res.Add(fileNode);
            }
            return res;
        }

        private List<FolderNode> LoadSubFolders() {
            if (!Exists) return new List<FolderNode>();
            var folders = Directory.GetDirectories(FullPath, "*", SearchOption.TopDirectoryOnly);
            return folders.Select(folder => new FolderNode(folder)).ToList();
        }

        public void Refresh() {
            Exists = Directory.Exists(FullPath);
            Files = LoadFiles();
            SubFolders = LoadSubFolders();
        }

        // Remove since it no longer makes sense
        public List<FolderNode> GetContent() {
            var res = new List<FolderNode> { new FolderNode(FullPath) };
            foreach (var folder in SubFolders) {
                res.AddRange(folder.GetContent().Select(f => new FolderNode(f.FullPath)));
            }
            return res;
        }

        public bool ContainsFolder(string name) {
            return SubFolders.Any(folder => folder.Name == name);
        }

        public bool ContainsFile(string name) {
            return GetFile(name) != null;
        }

        public FileNode GetFile(string name) {
            return Files.SingleOrDefault(x => x.Name == name);
        }

        public override bool Equals(object obj) {
            if (obj.GetType() != GetType()) return false;
            var other = obj as FolderNode;
            return other != null && FullPath == other.FullPath;
        }
    }
}