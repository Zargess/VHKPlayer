using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Model {
    public class FolderNode : IFolder {
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
            InitWatcher();
        }

        public bool Exists() {
            return Directory.Exists(FullPath);
        }

        private string GetFolderName(string path) {
            if (String.IsNullOrEmpty(path)) return "";
            if (!Exists()) return "";
            return Path.GetFileName(path);
        }

        private string GetSource() {
            if (!Exists()) return "";
            var temp = PathHandler.SplitPath(FullPath);
            return temp[temp.Length - 2];
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
            var temp = App.ConfigService.GetString("requiredFolders").Split(';');
            var folders = temp.Select(x => x.Replace("root", FullPath));

            foreach (var folder in folders) {
                if (!Directory.Exists(folder)) return false;
            }

            return true;
        }

        public bool InitWatcher() {
            if (Watcher != null) return false;
            if (!Exists()) return false;
            Watcher = new FileSystemWatcher {
                Path = FullPath,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastAccess
                    | NotifyFilters.LastWrite | NotifyFilters.DirectoryName,
                Filter = "*"
            };
            Watcher.Created += Changed;
            Watcher.Deleted += Changed;
            Watcher.Renamed += Changed;
            Watcher.EnableRaisingEvents = true;
            return true;
        }

        private void Changed(object sender, FileSystemEventArgs e) {
            Console.WriteLine(Name);
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
