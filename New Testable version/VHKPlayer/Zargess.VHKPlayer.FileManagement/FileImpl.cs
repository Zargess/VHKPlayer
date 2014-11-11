using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class FileImpl : IFile {
        public string FullPath { get; private set; }
        public string Name { get; private set; }
        public string Source { get; private set; }
        public FileType Type { get; private set; }

        public FileImpl(string path) {
            Name = GetName(path);
            FullPath = GetPath(path);
        }

        private string GetPath(string path) {
            var temp = path.Split('\\');
            return temp.Length > 1 ? path : Path.Combine(Environment.CurrentDirectory, path);
        }

        private string GetName(string path) {
            var temp = path.Split('\\');
            return temp[temp.Length - 1];
        }

        public bool Exists() {
            throw new NotImplementedException();
        }
    }
}
