using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public abstract class Node {
        public string Name { get; protected set; }
        public string Source { get; protected set; }
        public abstract string FullPath { get; set; }
    }
}
