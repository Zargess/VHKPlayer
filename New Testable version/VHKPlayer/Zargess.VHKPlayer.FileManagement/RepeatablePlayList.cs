using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class RepeatablePlayList : IPlayList {
        public Queue<IFile> Play() {
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
