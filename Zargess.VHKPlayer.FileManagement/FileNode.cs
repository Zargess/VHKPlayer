using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Settings;

namespace Zargess.VHKPlayer.FileManagement {
    public class FileNode {
        // TODO : Get duration of file if available
        public string Extension { get; private set; }
        public string NameWithNoExtension { get; private set; }
        public FileType Type { get; private set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string FullPath { get; set; }

        public FileNode(string path) {
            if (String.IsNullOrEmpty(path)) return;
            FullPath = path;
            var temp = PathHandler.SplitPath(FullPath);
            Name = temp[temp.Length - 1];
            if (temp.Length > 1) {
                Source = temp[temp.Length - 2];
            }
            var extens = FullPath.Split('.');
            Extension = extens[extens.Length - 1];
            var noextens = Name.Split('.').ToList();
            for (var i = 0; i < noextens.Count() - 1; i++) {
                NameWithNoExtension += noextens[i];
            }
            Type = GetFileType(Extension);
        }

        private FileType GetFileType(string e) {
            var t = e.ToLower();
            var pics = Utiliti.settingList("supportedPicture").ToList().Contains(t);
            var vids = Utiliti.settingList("supportedVideo").ToList().Contains(t);
            var mus = Utiliti.settingList("supportedMusic").ToList().Contains(t);

            if (pics) {
                return FileType.Picture;
            }

            if (vids) {
                return FileType.Video;
            }

            if (mus) {
                return FileType.Music;
            }

            return FileType.Unsupported;
        }

        public bool Exists() {
            return File.Exists(FullPath);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((FileNode)obj);
        }

        protected bool Equals(FileNode other) {
            return string.Equals(FullPath, other.FullPath) && string.Equals(Extension, other.Extension) && string.Equals(NameWithNoExtension, other.NameWithNoExtension) && Type == other.Type;
        }

        public override int GetHashCode() {
            unchecked {
                int hashCode = (FullPath != null ? FullPath.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Extension != null ? Extension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NameWithNoExtension != null ? NameWithNoExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)Type;
                return hashCode;
            }
        }
    }
}