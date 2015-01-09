using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

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
            Type = PathHandler.GetFileType(Path.GetExtension(path));
        }


        public bool Exists() {
            return File.Exists(FullPath);
        }

        public override bool Equals(object obj) {
            if (obj == this) return true;
            if (obj == null) return false;
            if (!(obj is FileNode)) return false;
            var other = obj as FileNode;
            return FullPath == other.FullPath;
        }
    }
}