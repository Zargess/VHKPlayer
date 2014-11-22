using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class Player : IPlayable {
        public ILoadingStrategy LoadingStrategy { get; private set; }

        public string Name { get; private set; }

        public int Size { get; private set; }

        public ObservableCollection<IFile> GetContent() {
            throw new NotImplementedException();
        }

        public Queue<IFile> Play(PlayType pt) {
            throw new NotImplementedException();
        }
    }
}
