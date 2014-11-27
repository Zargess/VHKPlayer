using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement {
    public class FolderNode : IFolder, IWatchable {
        public List<IFile> Content { get; private set; }
        public string FullPath { get; private set; }
        public string Name { get; private set; }
        public string Source { get; private set; }
        public FileSystemWatcher Watcher { get; private set; }

        public event EventHandler FolderChanged;

        public FolderNode(string path) {
            FullPath = GetFolderPath(path);
            Name = GetFolderName(path);
            Source = GetSource();
            Content = GetFiles();
        }

        public bool Exists() {
            return Directory.Exists(FullPath);
        }

        private string GetFolderName(string path) {
            if (String.IsNullOrEmpty(path)) return "";
            if (!Exists()) return "";
            return Path.GetFileName(path);
        }

        private List<IFile> GetFiles() {
            var res = new List<IFile>();
            if (!Exists()) return res;
            var paths = Directory.EnumerateFiles(FullPath);
            foreach (var path in paths) {
                var file = new FileNode(path);
                if (file.Type != FileType.Unsupported) {
                    res.Add(file);
                }
            }
            return res;
        }

        private string GetSource() {
            if (!Exists()) return "";
            var temp = PathHandler.SplitPath(FullPath);
            return temp[temp.Length - 2];
        }

        private string GetFolderPath(string path) {
            if (!Directory.Exists(path)) return "";
            var temp = PathHandler.SplitPath(path);
            return temp.Length > 1 ? path : Path.Combine(Environment.CurrentDirectory, path);
        }

        public bool ContainsFolder(IFolder folder) {
            var folders = Directory.EnumerateDirectories(FullPath).Select(x => new FolderNode(x));
            return folders.Any(f => string.Equals(f.FullPath, folder.FullPath, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool ContainsFile(IFile file) {
            return Content.Any(f => string.Equals(f.FullPath, file.FullPath, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool ValidRootFolder() {
            // TODO : Implement this method
            throw new NotImplementedException();
        }

        public bool InitWatcher() {
            if (Watcher != null) return false;
            if (!Exists()) return false;
            Watcher = new FileSystemWatcher {
                Path = FullPath,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastAccess
                    | NotifyFilters.LastWrite,
                Filter = "*.*"
            };
            Watcher.Created += Changed;
            Watcher.Deleted += Changed;
            Watcher.Renamed += Changed;
            Watcher.EnableRaisingEvents = true;
            return true;
        }

        private void Changed(object sender, FileSystemEventArgs e) {
            Content.Clear();
            foreach (var file in GetFiles()) {
                Content.Add(file);
            }
            if (FolderChanged == null) return;
            FolderChanged.Invoke(this, new EventArgs());
        }

        public bool StopWatcher() {
            if (Watcher == null) return false;
            Watcher.EnableRaisingEvents = false;
            Watcher.Dispose();
            Watcher = null;
            return true;
        }

        public IFolder Clone() {
            return new FolderNode(FullPath);
        }
    }
}