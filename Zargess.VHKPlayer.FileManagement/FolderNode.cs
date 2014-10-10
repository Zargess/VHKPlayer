using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.Settings;

namespace Zargess.VHKPlayer.FileManagement {
    public class FolderNode : Node {
        public List<FolderNode> SubFolders { get; private set; }
        public bool Exists { get; private set; }
        public ObservableSafeCollection<FileNode> Files { get; private set; }
        private string _fullpath;
        public override sealed string FullPath {
            get {
                return _fullpath;
            }
            set {
                if (String.IsNullOrEmpty(value)) return;
                _fullpath = value;
                var temp = PathHandler.SplitPath(_fullpath);
                Name = temp[temp.Length - 1];
                if (temp.Length > 1) Source = temp[temp.Length - 2];
            }
        }
        public FileSystemWatcher Watcher { get; set; }

        public FolderNode(string path) {
            FullPath = path;
            Exists = Directory.Exists(FullPath);
            Files = LoadFiles();
        }

        public void InitWatcher() {
            if (!Exists || Watcher != null) return;
            Watcher = new FileSystemWatcher {
                Path = FullPath,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastAccess
                    | NotifyFilters.LastWrite,
                Filter = "*.*"
            };
            Watcher.Created += Watcher_Created;
            Watcher.Deleted += Watcher_Deleted;
            Watcher.Renamed += Watcher_Renamed;
            Watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e) {
            var f = Files.SingleOrDefault(x => x.FullPath.Equals(e.OldFullPath));
            if (f == null) return;
            Files.Remove(f);
            Files.Add(new FileNode(e.FullPath));
            Files.Sort(p => p.Name);
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e) {
            var f = Files.SingleOrDefault(x => x.FullPath.Equals(e.FullPath));
            if (f == null) return;
            Files.Remove(f);
            Console.WriteLine(f.FullPath + " was deleted");
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e) {
            var file = new FileNode(e.FullPath);
            if (Files.Contains(file)) return;
            Files.Add(file);
            Console.WriteLine("Created " + e.FullPath);
            Files.Sort(p => p.Name);
        }

        private ObservableSafeCollection<FileNode> LoadFiles() {
            if (!Exists) return new ObservableSafeCollection<FileNode>();
            var temp = Directory.EnumerateFiles(FullPath)
                    .Select(x => new FileNode(x))
                    .Where(x => x.Type != FileType.Unsupported);
            var res = new ObservableSafeCollection<FileNode>();
            foreach (var fileNode in temp) {
                res.Add(fileNode);
            }
            return res;
        }

        private List<FolderNode> LoadSubFolders() {
            if (!Exists) return new List<FolderNode>();
            try {
                var folders = Directory.GetDirectories(FullPath, "*", SearchOption.TopDirectoryOnly);
                return folders.Select(folder => new FolderNode(folder)).ToList();
            } catch (UnauthorizedAccessException e) {
                Console.WriteLine("You do not have permission to use this folder. \nPlease choose another one.\n" + e.Message);
                return new List<FolderNode>();
            }
        }

        public void Refresh() {
            Exists = Directory.Exists(FullPath);
            Files = LoadFiles();;
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
            var folders = Directory.EnumerateDirectories(FullPath, "*", SearchOption.TopDirectoryOnly);
            name = name.ToLower();
            var ls = folders.Select(folder => folder.ToLower()).ToList();
            return ls.Select(PathHandler.SplitPath).Select(temp => temp[temp.Length - 1]).Any(n => name == n);
        }

        public bool ContainsFile(string name) {
            return GetFile(name) != null;
        }

        public FileNode GetFile(string name) {
            return Files.SingleOrDefault(x => x.Name == name);
        }

        public void StopListening() {
            Watcher.EnableRaisingEvents = false;
            Watcher.Dispose();
            Watcher = null;
        }

        public bool ValidRootFolder() {
            var requiredFolders = SettingsManager.GetSetting("requiredFolders") as string;
            if (requiredFolders == null) {
                Console.WriteLine("No such setting");
                return false;
            }
            var e = requiredFolders.Split(',');
            foreach (var s in e) {
                if (!ContainsFolder(s)) {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj) {
            if (obj.GetType() != GetType()) return false;
            var other = obj as FolderNode;
            return other != null && FullPath == other.FullPath;
        }
    }
}