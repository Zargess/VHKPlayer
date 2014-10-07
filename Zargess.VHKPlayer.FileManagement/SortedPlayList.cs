using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.LoadingPolicies;

namespace Zargess.VHKPlayer.FileManagement {
    public class SortedPlayList : PlayList {
        public int Index { get; private set; }


        public SortedPlayList(string name, int index) : base(name) {
            Index = index;
        }

        public SortedPlayList(string name, List<FileNode> content, int index) : base(name, content){
            Index = index;
        }

        public SortedPlayList(PlaylistLoading.Playlist list, int index) : base(list) {
            Index = index;
        }

        public override void InitWatcher() {
            throw new NotImplementedException();
        }
    }
}
