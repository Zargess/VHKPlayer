using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class RepeatablePlayList : IPlayList {
        public string Name { get; private set; }

        public RepeatablePlayList(string name) {
            Name = name;
        }

        public void Add(IPlayable p) {
            throw new NotImplementedException();
        }

        public Queue<IFile> Play(IFileSelectionStrategy selection) {
            throw new NotImplementedException();
        }

        public List<IFile> GetContent() {
            throw new NotImplementedException();
        }

        public void InitWatcher() {
            throw new NotImplementedException();
        }

        public void StopWatcher() {
            throw new NotImplementedException();
        }
    }
}
