using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Loading.PlayLists {
    public class FolderLoadingStrategy : ILoadingStrategy<IFile> {
        private IFolder Folder;

        public FolderLoadingStrategy(IFolder folder) {
            Folder = folder;
        }

        public void Load(ICollection<IFile> collection) {
            collection.Clear();
            foreach (var file in Folder.Content) {
                if (!file.Exists()) continue;
                if (file.Type == FileType.Unsupported) continue;
                collection.Add(file);
            }
        }
    }
}