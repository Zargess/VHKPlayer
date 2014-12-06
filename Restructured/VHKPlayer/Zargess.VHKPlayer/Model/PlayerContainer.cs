using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Factories.IPlayers;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Model {
    public class PlayerContainer : IContainer<IPlayable> {
        public IFolder Folder { get; private set; }
        public ObservableCollection<IPlayable> Content { get; private set; }
        public string Name { get; private set; }

        public PlayerContainer(string name, IFolder folder) {
            Folder = folder;
            Name = name;
            Content = new ObservableCollection<IPlayable>();
            var folders = App.ConfigService.GetString("playerFolders").Split(';').Select(x => PathHandler.AbsolutePath(x));
            foreach (var dir in folders) {
                var d = new FolderNode(dir);
                d.FolderChanged += (sender, ee) => Load();
            }
            Load();
        }

        public void Load() {
            Content.Clear();
            var files = Folder.Content.Where(x => x.Type == FileType.Picture);
            foreach (var file in files) {
                var player = new Player(new PlayerFactory(new FileNode(file.FullPath)));
                Content.Add(player);
            }
        }
    }
}
