using System.Collections.Generic;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Loading.IPlayables {
    public class FolderLoadingStrategy : ILoadingStrategy<IFile> {
        private IFolder Folder;

        public FolderLoadingStrategy(IFolder folder) {
            Folder = folder;
        }

        public void Load(ICollection<IFile> content) {
            content.Clear();
            foreach (var file in Folder.Content) {
                if (!file.Exists()) continue;
                if (file.Type == FileType.Unsupported) continue;
                content.Add(file);
            }
        }
    }
}