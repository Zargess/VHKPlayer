using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IQueuePeekStrategy {
        IFile HintNext(Queue<IFile> q, int index, IPlayable p);
    }
}
