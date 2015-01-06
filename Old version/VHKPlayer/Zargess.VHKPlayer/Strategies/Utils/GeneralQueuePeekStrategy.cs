using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Utils {
    public class GeneralQueuePeekStrategy : IQueuePeekStrategy {
        public IFile HintNext(Queue<IFile> q, int index, IPlayable p) {
            if (q.Count == 0) return null;
            return q.Peek();
        }
    }
}
