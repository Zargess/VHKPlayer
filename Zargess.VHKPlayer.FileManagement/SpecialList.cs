using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    class SpecialList : PlayList {
        private int Counter { get; set; }

        public SpecialList(string name) : base(name) {
            Counter = 0;
        }

        public SpecialList(string name, List<FileNode> content) : base(name, content) {
            Counter = 0;
        }

        public FileNode GetNext() {
            var file = Content[Counter];
            Counter = Counter == Content.Count ? 0 : Counter++;

            return file;
        }

        public FileNode GetCurrent() {
            if (Counter == Content.Count) {
                Counter = 0;
            }
            return Content[Counter];
        }
    }
}