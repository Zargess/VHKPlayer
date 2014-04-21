using System.Collections.Generic;
using System.Linq;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.FileManagement {
    public class File : IPlayAble {
        private string _fullpath;
        public string Name { get; private set; }
        public string Source { get; private set; }
        public string Extension { get; private set; }
        public string NameWithNoExtension { get; private set; }
        public FileType Type { get; private set; }
        public string FullPath {
            get {
                return _fullpath;
            }
            set {
                _fullpath = value;
                var temp = PathHandler.SplitPath(_fullpath);
                Name = temp[temp.Length - 1];
                if (temp.Length > 1) {
                    Source = temp[temp.Length - 2];
                }
                var extens = _fullpath.Split('.');
                Extension = extens[extens.Length - 1];
                var noextens = Name.Split('.');
                for (var i = 0; i < noextens.Count() - 1; i++) {
                    NameWithNoExtension += noextens[i];
                }
                SetFileType(Extension);
            }
        }

        public File(string path) {
            FullPath = path;
        }

        private void SetFileType(string e) {
            switch (e) {
                case "jpg":
                case "png":
                    Type = FileType.Picture;
                    break;
                case "avi":
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

        public IEnumerable<IPlayAble> GetContent() {
            return new List<IPlayAble> {this};
        }
    }
}
