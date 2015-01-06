using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Model {
    public class FileNode : IFile {
        public string FullPath { get; private set; }
        public string Name { get; private set; }
        public string Source { get; private set; }
        public FileType Type { get; private set; }
        public string NameWithoutExtension { get; private set; }

        public FileNode(string path) {
            Name = PathHandler.GetFileName(path);
            FullPath = PathHandler.GetPath(path);
            Source = PathHandler.GetSource(path);
            Type = PathHandler.GetFileType(Name);
            NameWithoutExtension = PathHandler.GetNameWithoutExtension(Name);
        }

        public bool Exists() {
            return File.Exists(FullPath);
        }
    }
}