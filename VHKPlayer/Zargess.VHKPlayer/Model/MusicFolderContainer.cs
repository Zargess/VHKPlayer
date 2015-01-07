using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Model {
    public class MusicFolderContainer : IContainer<IContainer<IPlayable>> {
        public ObservableCollection<IContainer<IPlayable>> Content { get; private set; }
        private IFolder Folder { get; set; }
        private Dispatcher Disp { get; set; }

        public string Name {
            get {
                return "Music";
            }
        }

        public MusicFolderContainer(IFolder folder) {
            Folder = folder;
            Content = new SortableCollection<IContainer<IPlayable>>();
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
