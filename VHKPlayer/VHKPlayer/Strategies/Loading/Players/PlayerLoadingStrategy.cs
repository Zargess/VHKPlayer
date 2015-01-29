using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Utility;

namespace VHKPlayer.Strategies.Loading.Players {
    public class PlayerLoadingStrategy : ILoadingStrategy<IFile> {
        private IFile _file;

        public PlayerLoadingStrategy(IFile file) {
            _file = file;
        }

        public void Load(ICollection<IFile> collection) {
            collection.Clear();
            var folders = GeneralFunctions.GetPlayerFolderPaths().Select(x => new FolderNode(x));

            foreach (var folder in folders) {
                //var f = folder.Content.SingleOrDefault(x => x.NameWithoutExtension.ToLower() == _file.NameWithoutExtension.ToLower());
                IFile f = null;
                foreach (var file in folder.Content) {
                    if (_file.NameWithoutExtension.ToLower() != file.NameWithoutExtension.ToLower()) continue;
                    f = file;
                    break;
                }
                if (f == null) continue;
                collection.Add(f);
            }

            // TODO : Check if player is missing any files and create notification if he does

        }
    }
}
