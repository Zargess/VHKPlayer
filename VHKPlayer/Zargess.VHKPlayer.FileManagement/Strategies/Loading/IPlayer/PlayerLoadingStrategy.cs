using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Loading.IPlayer {
    public class PlayerLoadingStrategy : ILoadingStrategy {
        private IFile _file;

        public PlayerLoadingStrategy(IFile file) {
            _file = file;
        }

        public void Load(ICollection<IFile> content) {
            var temp = SettingsManagement.GetStringSetting("playerFolders").Split(';');
            var folders = temp.Select(x => new FolderNode(PathHandler.AbsolutePath(x)));
            foreach (var folder in folders) {
                var file = folder.Content.SingleOrDefault(x => x.NameWithoutExtension.ToLower() == _file.NameWithoutExtension.ToLower());
                if (file == null) continue;
                content.Add(file);
            }
        }
    }
}
