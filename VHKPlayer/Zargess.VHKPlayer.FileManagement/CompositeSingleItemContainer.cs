using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Threading;
using Zargess.VHKPlayer.FileManagement.Collections;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement {
    public class CompositeSingleItemContainer : ICompositeContainer {
        public ObservableCollection<IContainer> Content { get; private set; }
        private IFolder Folder { get; set; }
        private Dispatcher Disp { get; set; }

        public CompositeSingleItemContainer(IFolder folder) {
            Folder = folder;
            Content = new SortableCollection<IContainer>();
            Disp = Dispatcher.CurrentDispatcher;
            Action action = () => {
                Load();
            };
            folder.FolderChanged += (sender, ee) => Disp.BeginInvoke(action);
            Load();
        }

        public void Load() {
            Content.Clear();
            var temp = Directory.EnumerateDirectories(Folder.FullPath, "*", SearchOption.TopDirectoryOnly);
            var folders = temp.Select(x => new FolderNode(x));
            foreach (var folder in folders) {
                Content.Add(new SingleItemContainer(folder));
            }
        }
    }
}