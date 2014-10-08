using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.LoadingPolicies;

namespace Zargess.VHKPlayer.FileManagement {
    public abstract class PlayList : IWatchable {
        // TODO : Rethink the entire concept of playlists. How to get the next element in the list? When do we stop playing this playlist? Should this be controlled in a videoqueue object?
        public List<FileNode> Content { get; private set; }
        public string Name { get; private set; }
        public FileSystemWatcher Watcher { get; protected set; }
        public FolderNode Folder { get; protected set; }

        protected PlayList(string name, FolderNode folder) {
            Name = name;
            Content = new List<FileNode>();
            Folder = folder;
            InitWatcher();
        }

        protected PlayList(string name, FolderNode folder, List<FileNode> content) : this(name, folder) {
            AddRange(content);
        }

        protected PlayList(PlaylistLoading.Playlist list, FolderNode folder) : this(list.Name, folder) {
            list.Content.ToList().ForEach(x => Add(new FileNode(x.Path)));
        }

        public void AddRange(List<FileNode> list) {
            list.ForEach(x => Add(new FileNode(x.FullPath)));
        }

        public void Add(FileNode file) {
            Content.Add(new FileNode(file.FullPath));
        }
        // TODO : For each of the subtypes implement a method to get the content to play. Consider adding an abstract method with the following signature:
        // public abstract object GetContent();     or     public abstract List<FileNode> GetContent();
        // Otherwise let the future playmanager handle the different cases by checking the given type of playlist.
        public FileNode GetFile(int index) {
            return Content[index];
        }

        public void InitWatcher() {
            if (!Folder.Exists || Watcher != null) return;
            Watcher = new FileSystemWatcher {
                Path = Folder.FullPath,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastAccess
                    | NotifyFilters.LastWrite,
                Filter = "*.*"
            };
            Watcher.Created += OnCreated;
            Watcher.Deleted += OnDeleted;
            Watcher.Renamed += OnRenamed;
            Watcher.EnableRaisingEvents = true;
        }

        public void StopListening() {
            if (Watcher == null) return;
            Watcher.EnableRaisingEvents = false;
            Watcher.Dispose();
            Watcher = null;
        }

        public abstract void Refresh();

        protected void OnCreated(object sender, FileSystemEventArgs e) {
            Refresh();
        }

        protected void OnRenamed(object sender, RenamedEventArgs e) {
            Refresh();
        }

        protected void OnDeleted(object sender, FileSystemEventArgs e) {
            Refresh();
        }
    }
}