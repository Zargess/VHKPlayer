using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement {
    public class CompositeSingleItemContainer : ICompositeContainer {
        public ObservableCollection<IContainer> Content { get; private set; }
        private IFolder Folder { get; set; }

        public CompositeSingleItemContainer(IFolder folder) {
            Folder = folder;
            Content = new ObservableCollection<IContainer>();
            folder.FolderChanged += (sender, ee) => Load();
            Load();
        }

        public void Load() {
            // TODO : Make some sort method depending on settings
            Content.Clear();
            var temp = Directory.EnumerateDirectories(Folder.FullPath, "*", SearchOption.TopDirectoryOnly);
            var folders = temp.Select(x => new FolderNode(x));
            foreach (var folder in folders) {
                Content.Add(new SingleItemContainer(folder));
            }
        }
    }
}
