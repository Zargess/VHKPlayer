using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Strategies.Loading.IPlayables;

namespace Zargess.VHKPlayer.Model {
    public class SingleItemContainer : IContainer<IPlayable> {
        public ObservableCollection<IPlayable> Content { get; private set; }
        public string Name { get; private set; }
        public IFolder Folder { get; private set; }
        public Dispatcher Disp { get; private set; }

        public SingleItemContainer(IFolder folder) {
            Folder = folder;
            Name = folder.Name;
            Content = new SortableCollection<IPlayable>();
            Disp = Dispatcher.CurrentDispatcher;
            Action action = () => {
                Load();
            };
            Folder.FolderChanged += (sender, ee) => Disp.BeginInvoke(action);
            Load();
        }

        public void Load() {
            Content.Clear();
            foreach (var file in Folder.Content) {
                if (file.Name == null) continue;
                if (file.Type == FileType.Info) continue;
                Content.Add(new SingleItemPlayable(new FileLoadingStrategy(file.FullPath)));
            }
        }
    }
}
