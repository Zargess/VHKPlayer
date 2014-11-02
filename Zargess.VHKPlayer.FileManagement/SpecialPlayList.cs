using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.LoadingPolicies;

namespace Zargess.VHKPlayer.FileManagement {
    public class SpecialPlayList : PlayList {
        public int Counter { get; private set; }
        // TODO : Consider setting a boolean if a playlist can be played as a sequence or not.
        public SpecialPlayList(string name, FolderNode folder)
            : base(name, folder) {
            Counter = 0;
        }

        public SpecialPlayList(string name, FolderNode folder, List<FileNode> content)
            : base(name, folder, content) {
            Counter = 0;
        }

        public SpecialPlayList(PlaylistLoading.Playlist list, FolderNode folder)
            : base(list, folder) {
            Counter = 0;
        }

        public FileNode GetNext() {
            if (Counter >= Content.Count) Counter = 0;
            var file = Content[Counter];
            if (Counter >= Content.Count) {
                Counter = 0;
            } else {
                Counter++;
            }

            return file;
        }

        public FileNode GetCurrent() {
            if (Counter == Content.Count) Counter = 0;
            return Content[Counter];
        }

        public override List<FileNode> GetContent() {
            try {
                return new List<FileNode> { Content[Counter] };
            } catch (ArgumentOutOfRangeException) {
                Counter = 0;
                return new List<FileNode> { Content[Counter] };
            } finally {
                Counter++;
            }
        }

        public override void Refresh() {
            Content.Clear();
            var list = PlaylistLoading.playlistFromFolderContent(Name, Folder.FullPath);
            list.Content.ToList().ForEach(x => Content.Add(new FileNode(x.Path)));
        }
    }
}