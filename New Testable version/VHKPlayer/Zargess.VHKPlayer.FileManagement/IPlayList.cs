using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public interface IPlayList : IPlayable, IWatchable {
        FileSystemWatcher Watcher { get; }

        void Add(IFile p);
    }
}
