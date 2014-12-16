using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Utils {
    public class IteratedQueuePeekStrategy : IQueuePeekStrategy {
        public IFile HintNext(Queue<IFile> q, int index, IPlayable p) {
            int i = index;
            if (index >= p.Content.Count) i = 0;
            return p.Content[i];
        }
    }
}
