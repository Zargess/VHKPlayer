using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.Strategies.Loading.IPlayables {
    public class FileLoadingStrategy : ILoadingStrategy<IFile> {
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
