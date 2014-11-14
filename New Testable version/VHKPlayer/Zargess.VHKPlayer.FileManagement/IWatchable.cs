using System;
using System.IO;

namespace Zargess.VHKPlayer.FileManagement {
    public interface IWatchable {
        FileSystemWatcher Watcher { get; }
        bool InitWatcher();
        bool StopWatcher();
    }
}
