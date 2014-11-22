using System.Collections.ObjectModel;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.FileManagement.Strategies.Loading.IPlayable;

namespace Zargess.VHKPlayer.FileManagement {
    public class SingleItemContainer : IContainer {
        public ObservableCollection<IPlayable> Content { get; private set; }
        public string Name { get; private set; }
        public IFolder Folder { get; private set; }

        public SingleItemContainer(IFolder folder) {
            Folder = folder;
            Name = folder.Name;
            Content = new ObservableCollection<IPlayable>();
            Folder.FolderChanged += (sender, ee) => Load();
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