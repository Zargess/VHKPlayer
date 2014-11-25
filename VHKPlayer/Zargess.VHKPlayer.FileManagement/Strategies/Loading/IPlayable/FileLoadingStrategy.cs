using System.Collections.Generic;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Loading.IPlayable {
    public class FileLoadingStrategy : ILoadingStrategy {
        private string Path { get; set; }

        public FileLoadingStrategy(string path) {
            Path = path;
        }

        public void Load(ICollection<IFile> content) {
            var file = new FileNode(Path);
            if (!file.Exists()) return;
            if (file.Type == FileType.Unsupported) return;
            content.Add(file);
        }
    }
}