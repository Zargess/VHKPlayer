using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Zargess.VHKPlayer.FileManagement.Collections;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.FileManagement.Strategies.Loading.IPlayables;

namespace Zargess.VHKPlayer.FileManagement {
    public class SingleItemContainer : IContainer {
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