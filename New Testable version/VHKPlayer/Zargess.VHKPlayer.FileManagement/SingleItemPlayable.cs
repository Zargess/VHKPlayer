using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class SingleItemPlayable : IPlayable {
        public ObservableCollection<IFile> Content { get; private set; }
        public ILoadingStrategy LoadingStrategy { get; private set; }
        public string Name { get; private set; }

        public SingleItemPlayable(ILoadingStrategy loadingStrategy) {
            Content = new ObservableCollection<IFile>();
            LoadingStrategy = loadingStrategy;
            LoadingStrategy.Load(Content);
            if (Content.Count <= 0) return;
            Name = Content[0].Name;
        }

        public Queue<IFile> Play() {
            throw new NotImplementedException();
        }
    }
}
