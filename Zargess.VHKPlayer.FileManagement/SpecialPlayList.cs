using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.LoadingPolicies;

namespace Zargess.VHKPlayer.FileManagement {
    public class SpecialPlayList : PlayList {
        // TODO : Consider implementing a filesystemwatcher to look for changes
        public int Counter { get; private set; }

        public SpecialPlayList(string name, FolderNode folder) : base(name, folder) {
            Counter = 0;
        }

        public SpecialPlayList(string name, FolderNode folder, List<FileNode> content) : base(name, folder, content) {
            Counter = 0;
        }

        public SpecialPlayList(PlaylistLoading.Playlist list, FolderNode folder) : base(list, folder) {
            Counter = 0;
        }

        public FileNode GetNext() {
            var file = Content[Counter];
            Counter = Counter == Content.Count ? 0 : Counter++;

            return file;
        }

        public FileNode GetCurrent() {
            if (Counter == Content.Count) Counter = 0;
            return Content[Counter];
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