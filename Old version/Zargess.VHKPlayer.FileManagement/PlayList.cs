﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.LoadingPolicies;

namespace Zargess.VHKPlayer.FileManagement {
    public abstract class PlayList : IWatchable, IEnumerable<FileNode> {
        public SortableCollection<FileNode> Content { get; private set; }
        public string Name { get; private set; }
        public FileSystemWatcher Watcher { get; protected set; }
        public FolderNode Folder { get; protected set; }

        protected PlayList(string name, FolderNode folder) {
            Name = name;
            Content = new SortableCollection<FileNode>();
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

        public abstract List<FileNode> GetContent(); 

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

        public IEnumerator<FileNode> GetEnumerator() {
            return Content.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public override string ToString() {
            return Name;
        }
    }
}