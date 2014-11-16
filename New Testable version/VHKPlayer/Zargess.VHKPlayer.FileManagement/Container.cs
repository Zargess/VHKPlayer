using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class Container : IContainer {

        public ObservableCollection<IPlayable> Content { get; private set; }

        public IFolder Folder { get; private set; }

        public string Name { get; private set; }

        public Container(IFolder folder) {
            Folder = folder;
            Name = Folder.Name;
            //Content = Load();
        }

        public ObservableCollection<IPlayable> Load() {
            return null;
        }
    }
}
