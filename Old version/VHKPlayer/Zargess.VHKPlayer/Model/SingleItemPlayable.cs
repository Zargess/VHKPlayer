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


        public IFileSelectionStrategy SelectionStrategy { get; private set; }

        public SingleItemPlayable(ISingleItemPlayableFactory factory) {
            Content = new ObservableCollection<IFile>();
            LoadingStrategy = factory.CreateLoadingStrategy();
            SelectionStrategy = factory.CreateSelectionStrategy();
            LoadingStrategy.Load(Content);
            if (Content.Count <= 0) return;
            Name = Content[0].Name;
        }

        public Queue<IFile> Play(PlayType pt) {
            return SelectionStrategy.SelectFiles(this, pt);
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
