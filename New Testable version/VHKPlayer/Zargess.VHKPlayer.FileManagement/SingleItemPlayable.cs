using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Zargess.VHKPlayer.FileManagement {
    public class SingleItemPlayable : IPlayable {
        private ObservableCollection<IFile> Content { get; set; }
        public ILoadingStrategy LoadingStrategy { get; private set; }
        public string Name { get; private set; }

        public int Size {
            get {
                return Content.Count;
            }
        }

        public SingleItemPlayable(ILoadingStrategy loadingStrategy) {
            Content = new ObservableCollection<IFile>();
            LoadingStrategy = loadingStrategy;
            LoadingStrategy.Load(Content);
            if (Content.Count <= 0) return;
            Name = Content[0].Name;
        }

        public Queue<IFile> Play() {
            var res = new Queue<IFile>();
            if (Content.Count > 0) res.Enqueue(new FileNode(Content[0].FullPath));
            return res;
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