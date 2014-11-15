using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayList : IPlayList {
        private IFileSelectionStrategy SelectionStrategy { get; set; }
        public ILoadingStrategy LoadingStrategy { get; private set; }
        public string Name { get; private set; }
        public ObservableCollection<IFile> Content { get; private set; }
        private IFolder Folder { get; set; }

        public PlayList(string name, IFolder folder, IFileSelectionStrategy selectionStrategy, ILoadingStrategy loadingStrategy) {
            Name = name;
            Content = new ObservableCollection<IFile>();
            SelectionStrategy = selectionStrategy;
            Folder = folder;
            Folder.FolderChanged += FolderChanged;
            LoadingStrategy = loadingStrategy;
            LoadingStrategy.Load(Content);
        }

        private void FolderChanged(object sender, EventArgs e) {
            LoadingStrategy.Load(Content);
        }

        public void Add(IFile file) {
            Content.Add(file);
        }

        public Queue<IFile> Play() {
            return SelectionStrategy.SelectFiles(this);
        }
    }
}