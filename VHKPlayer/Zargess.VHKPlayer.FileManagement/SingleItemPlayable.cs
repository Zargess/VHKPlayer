using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement {
    public class SingleItemPlayable : IPlayable {
        private List<IFile> Content { get; set; }
        private ILoadingStrategy<IFile> LoadingStrategy { get; set; }
        public string Name { get; private set; }

        public int Size {
            get {
                return Content.Count;
            }
        }

        public SingleItemPlayable(ILoadingStrategy<IFile> loadingStrategy) {
            Content = new List<IFile>();
            LoadingStrategy = loadingStrategy;
            LoadingStrategy.Load(Content);
            if (Content.Count <= 0) return;
            Name = Content[0].Name;
        }

        public Queue<IFile> Play(PlayType pt) {
            var res = new Queue<IFile>();
            if (Content.Count > 0) res.Enqueue(Content[0].Clone());
            return res;
        }

        public ObservableCollection<IFile> GetContent() {
            var res = new ObservableCollection<IFile>();
            foreach (var file in Content) {
                res.Add(file.Clone());
            }
            return res;
        }
    }
}