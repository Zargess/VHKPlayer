using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Models {
    public class FileNode : IFile {
        public string FullPath { get; private set; }
        public string Name { get; private set; }
        public string NameWithoutExtension { get; private set; }
        public FileType Type { get; private set; }

        public FileNode(string path) {
            FullPath = path;
            Name = Path.GetFileName(path);
            NameWithoutExtension = Path.GetFileNameWithoutExtension(path);
            Type = FileType.Music;
        }


        public bool Exists() {
            throw new NotImplementedException();
        }
    }
}