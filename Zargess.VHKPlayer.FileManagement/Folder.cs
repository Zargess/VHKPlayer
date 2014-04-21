using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class Folder : IEnumerable<File> {
        public string Name { get; private set; }
        public string Source { get; private set; }
        public List<File> Content { get; private set; } 

        private string _fullpath;
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

                RefreshFiles(_fullpath);
            }
        }

        public Folder(string path) {
            FullPath = path;
        }

        private void RefreshFiles(string path) {
            foreach (var file in Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly)) {
                Content.Add(new File(file));
            }
        }

        public void Refresh() {
            Content.Clear();
            RefreshFiles(_fullpath);
        }

        public void AddFile(File fn) {
            Content.Add(fn);
        }

        public IEnumerator<File> GetEnumerator() {
            return Content.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
