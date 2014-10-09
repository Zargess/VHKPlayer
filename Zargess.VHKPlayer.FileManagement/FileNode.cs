using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class FileNode : Node {
        // TODO : Get duration of file if available
        private string _fullpath;
        public string Extension { get; private set; }
        public string NameWithNoExtension { get; private set; }
        public FileType Type { get; private set; }
        public override sealed string FullPath {
            get {
                return _fullpath;
            }
            set {
                if (String.IsNullOrEmpty(value)) return;
                _fullpath = value;
                var temp = PathHandler.SplitPath(_fullpath);
                Name = temp[temp.Length - 1];
                if (temp.Length > 1) {
                    Source = temp[temp.Length - 2];
                }
                var extens = _fullpath.Split('.');
                Extension = extens[extens.Length - 1];
                var noextens = Name.Split('.').ToList();
                for (var i = 0; i < noextens.Count() - 1; i++) {
                    NameWithNoExtension += noextens[i];
                }
                SetFileType(Extension);
            }
        }

        // TODO : Rewrite this so that the user can define what types of files is supported
        private void SetFileType(string e) {
            var t = e.ToLower();
            switch (t) {
                case "jpg":
                case "png":
                    Type = FileType.Picture;
                    break;
                case "avi":
                case "mp4":
                    Type = FileType.Video;
                    break;
                case "mp3":
                    Type = FileType.Music;
                    break;
                default:
                    Type = FileType.Unsupported;
                    break;
            }
        }

        public FileNode(string path) {
            FullPath = path;
        }

        public bool Exists() {
            return File.Exists(FullPath);
        }

        public override bool Equals(object obj) {
            if (!(obj is FileNode)) { return false; }
            var other = obj as FileNode;
            return _fullpath == other._fullpath;
        }
    }
}