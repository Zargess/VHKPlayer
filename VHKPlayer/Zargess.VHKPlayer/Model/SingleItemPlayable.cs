using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Model {
    public class SingleItemPlayable : IPlayable {
        public ObservableCollection<IFile> Content { get; set; }
        private ILoadingStrategy<IFile> LoadingStrategy { get; set; }
        public string Name { get; private set; }
        public bool Repeat {
            get {
                return false;
            }

            set { }
        }

        public SingleItemPlayable(ILoadingStrategy<IFile> loadingStrategy) {
            Content = new ObservableCollection<IFile>();
            LoadingStrategy = loadingStrategy;
            LoadingStrategy.Load(Content);
            if (Content.Count <= 0) return;
            Name = Content[0].Name;
        }

        public Queue<IFile> Play(PlayType pt) {
            var res = new Queue<IFile>();
            if (Content.Count > 0) res.Enqueue(Content[0]);
            return res;
        }

        public ObservableCollection<IFile> GetContent() {
            var res = new ObservableCollection<IFile>();
            foreach (var file in Content) {
                res.Add(file);
            }
            return res;
        }

        public override string ToString() {
            return Name;
        }
    }
}
