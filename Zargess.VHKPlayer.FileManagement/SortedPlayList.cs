using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.LoadingPolicies;

namespace Zargess.VHKPlayer.FileManagement {
    public class SortedPlayList : PlayList {
        public SortedPlayList(string name, int index) : base(name) {
        }

        public SortedPlayList(string name, List<FileNode> content, int index) : base(name, content) {
        }

        public SortedPlayList(PlaylistLoading.Playlist list, int index) : base(list) {
        }
    }
}
