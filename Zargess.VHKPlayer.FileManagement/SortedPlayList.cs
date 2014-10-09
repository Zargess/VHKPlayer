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

        public override void Refresh() {
            Content.Clear();
            var list = PlaylistLoading.sortedPlaylist(Folder.FullPath, Name, Index);
            list.Content.ToList().ForEach(x => Content.Add(new FileNode(x.Path)));
        }

        public List<FileNode> GetContent() {
            return Content;
        }
    }
}
