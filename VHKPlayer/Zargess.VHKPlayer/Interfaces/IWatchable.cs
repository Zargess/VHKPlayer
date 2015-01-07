using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IWatchable {
        FileSystemWatcher Watcher { get; }
        bool InitWatcher();
        bool StopWatcher();
    }
}
