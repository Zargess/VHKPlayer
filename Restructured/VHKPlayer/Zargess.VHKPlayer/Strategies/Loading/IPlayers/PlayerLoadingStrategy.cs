using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Exceptions;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Strategies.Loading.IPlayers {
    public class PlayerLoadingStrategy : ILoadingStrategy<IFile> {
        private IFile _file;

        public PlayerLoadingStrategy(IFile file) {
            _file = file;
        }

        public void Load(ICollection<IFile> content) {
            var temp = App.ConfigService.GetString("playerFolders").Split(';');
            var folders = temp.Select(x => new FolderNode(PathHandler.AbsolutePath(x)));
            foreach (var folder in folders) {
                var file = folder.Content.SingleOrDefault(x => x.NameWithoutExtension.ToLower() == _file.NameWithoutExtension.ToLower());
                if (file == null) continue;
                content.Add(file);
            }
            foreach(var folder in folders) {
                if(!content.Any(x => x.FullPath.Contains(folder.FullPath))) {
                    // TODO : Make notification system instead of throwing exception
                    //throw new FilesMissingException("Der mangler en fil i: " + folder.FullPath + " til " + _file.NameWithoutExtension);
                }
            }
        }
    }
}
