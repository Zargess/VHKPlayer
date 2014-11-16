using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayList : IPlayList {
        private IFileSelectionStrategy SelectionStrategy { get; set; }
        public ILoadingStrategy LoadingStrategy { get; private set; }
        public string Name { get; private set; }
        private ObservableCollection<IFile> Content { get; set; }
        private IFolder Folder { get; set; }

        public int Size {
            get {
                return Content.Count;
            }
        }

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

        public ObservableCollection<IFile> GetContent() {
            var res = new ObservableCollection<IFile>();
            foreach (var file in Content) {
                res.Add(new FileNode(file.FullPath));
            }
            return res;
        }
    }
}