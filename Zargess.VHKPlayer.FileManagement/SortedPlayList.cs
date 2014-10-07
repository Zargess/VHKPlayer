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


        public SortedPlayList(string name, int index, FolderNode folder) : base(name, folder) {
            Index = index;
        }

        public SortedPlayList(string name, List<FileNode> content, int index, FolderNode folder) : base(name, folder, content) {
            Index = index;
        }

        public SortedPlayList(PlaylistLoading.Playlist list, int index, FolderNode folder) : base(list, folder) {
            Index = index;
        }

        protected override void OnCreated(object sender, FileSystemEventArgs e) {
            throw new NotImplementedException();
        }

        protected override void OnRenamed(object sender, RenamedEventArgs e) {
            throw new NotImplementedException();
        }

        protected override void OnDeleted(object sender, FileSystemEventArgs e) {
            throw new NotImplementedException();
        }
    }
}
