using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Factories.Player;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayerContainer : IContainer {
        public IFolder Folder { get; private set; }
        public ObservableCollection<IPlayable> Content { get; private set; }
        public string Name { get; private set; }
        public bool IncludeTrainers { get; private set; }

        public PlayerContainer(string name, IFolder folder, bool includeTrainers) {
            Folder = folder;
            Name = name;
            IncludeTrainers = includeTrainers;
            Content = new ObservableCollection<IPlayable>();
            var folders = SettingsManagement.Instance.GetStringSetting("playerFolders").Split(';').Select(x => PathHandler.AbsolutePath(x));
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
                if (player.Trainer && !IncludeTrainers) continue;
                Content.Add(player);
            }
        }
    }
}