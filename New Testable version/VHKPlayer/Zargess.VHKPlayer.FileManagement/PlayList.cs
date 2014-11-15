﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayList : IPlayList {
        private IFileSelectionStrategy SelectionStrategy { get; set; }
        public string Name { get; private set; }
        public ObservableCollection<IFile> Content { get; private set; }
        public FileSystemWatcher Watcher { get; private set; }
        private IFolder Folder { get; set; }
        public ILoadingStrategy LoadingStrategy { get; private set; }

        public PlayList(string name, IFolder folder, IFileSelectionStrategy selectionStrategy) {
            Name = name;
            Content = new ObservableCollection<IFile>();
            SelectionStrategy = selectionStrategy;
            Folder = folder;
        }

        public void Add(IFile file) {
            Content.Add(file);
        }

        public Queue<IFile> Play() {
            return SelectionStrategy.SelectFiles(this);
        }

        public bool InitWatcher() {
            if (Watcher != null) return false;
            if (!Folder.Exists) return false;
            Watcher = new FileSystemWatcher {
                Path = Folder.FullPath,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastAccess
                    | NotifyFilters.LastWrite,
                Filter = "*.*"
            };
            Watcher.Created += RaisedEvent;
            Watcher.Deleted += RaisedEvent;
            Watcher.Renamed += RaisedEvent;
            Watcher.EnableRaisingEvents = true;

            return true;
        }

        private void RaisedEvent(object sender, FileSystemEventArgs e) {
            LoadingStrategy.load(Content);
        }

        public bool StopWatcher() {
            if (Watcher == null) return false;
            Watcher.EnableRaisingEvents = false;
            Watcher.Dispose();
            Watcher = null;
            return true;
        }
    }
}