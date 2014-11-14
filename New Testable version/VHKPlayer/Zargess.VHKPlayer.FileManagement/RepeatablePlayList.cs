using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class RepeatablePlayList : IPlayList {
        public string Name { get; private set; }
        public ObservableCollection<IFile> Content { get; private set; }

        public RepeatablePlayList(string name) {
            Name = name;
            Content = new ObservableCollection<IFile>();
        }

        public void Add(IFile file) {
            Content.Add(file);
        }

        public Queue<IFile> Play(IFileSelectionStrategy selection) {
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
