using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Utility.FindFileType.Interfaces;

namespace VHKPlayer.Models
{
    public class FileNode
    {
        public string FullPath { get; set; }
        public string Name { get; set; }
        public string NameWithoutExtension { get; set; }
        public FileType Type { get; set; }

        public bool Exists()
        {
            return File.Exists(FullPath);
        }

        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (obj == null) return false;
            if (!(obj is FileNode)) return false;
            var other = obj as FileNode;
            var path = FullPath.ToLower();
            var otherpath = other.FullPath.ToLower();
            return FullPath.ToLower() == other.FullPath.ToLower();
        }

        public override int GetHashCode()
        {
            return FullPath.GetHashCode();
        }
    }
}
