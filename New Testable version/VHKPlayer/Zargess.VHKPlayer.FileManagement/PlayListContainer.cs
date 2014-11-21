using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayListContainer : IContainer {
        public ObservableCollection<IPlayable> Content { get; set; }
        public IFolder Folder { get; set; }
        public string Name { get; set; }

        public PlayListContainer(IFolder folderNode) {
            Folder = folderNode;
            Content = new ObservableCollection<IPlayable>();
            Name = "PlayLister";
        }

        public void Load() {
            Content.Clear();
        }

        public List<IPlayList> AllFilesSorted() {
            throw new NotImplementedException();
        }
    }
}
