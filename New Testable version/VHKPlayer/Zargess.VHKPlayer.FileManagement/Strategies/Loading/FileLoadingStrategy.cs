using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Loading {
    public class FileLoadingStrategy : ILoadingStrategy {
        private string Path { get; set; }

        public FileLoadingStrategy(string path) {
            Path = path;
        }

        public void Load(ICollection<IFile> content) {
            var file = new FileNode(Path);
            if (!file.Exists()) return;
            content.Add(file);
        }
    }
}
