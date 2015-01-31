using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Models {
    public class FolderNode : IFolder {
        private List<IFolderObserver> _observers;
        private FileSystemWatcher _watcher;
        public List<IFile> Content { get; private set; }
        public string FullPath { get; private set; }
        public string Name { get; private set; }

        public FolderNode(string path) {
            FullPath = path;
            Name = Path.GetFileName(path);
            Content = GetFiles();
            _observers = new List<IFolderObserver>();
            InitWatcher();
        }

        private List<IFile> GetFiles() {
            var res = new List<IFile>();
            if (!Exists()) return res;
            var paths = Directory.EnumerateFiles(FullPath);
            foreach (var path in paths) {
                var file = new FileNode(path);
                if (file.Type == FileType.Unsupported) continue;
                res.Add(file);
            }
            return res;
        }

        public bool ContainsFile(IFile file) {
            foreach (var f in Content) {
                if (f.Equals(file)) return true;
            }
            return false;
        }

        public bool Exists() {
            return Directory.Exists(FullPath);
        }

        public bool ValidRootFolder() {
            var paths = Settings.RequiredFolders.Select(x => x.Replace("root", FullPath));

            foreach (var path in paths) {
                if (!Directory.Exists(path)) return false;
            }

            return true;
        }

        public void AddObserver(IFolderObserver observer) {
            _observers.Add(observer);
        }

        public void RemoveObserver(IFolderObserver observer) {
            _observers.Remove(observer);
        }

        private void InitWatcher() {
            if (_watcher != null) return;
            if (!Exists()) return;
            _watcher = new FileSystemWatcher {
                Path = FullPath,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastAccess
                    | NotifyFilters.LastWrite | NotifyFilters.DirectoryName,
                Filter = "*"
            };
            _watcher.Created += Changed;
            _watcher.Deleted += Changed;
            _watcher.Renamed += Changed;
            _watcher.EnableRaisingEvents = true;
        }

        private void Changed(object sender, FileSystemEventArgs e) {
            Content.Clear();
            Content.AddRange(GetFiles());
            _observers.ForEach(x => x.FolderChanged(this));
        }
    }
}
