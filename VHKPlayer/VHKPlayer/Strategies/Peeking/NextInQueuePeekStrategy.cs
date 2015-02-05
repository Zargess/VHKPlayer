using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Peeking {
    public class NextInQueuePeekStrategy : IPeekStrategy {
        public IFile Peek(IPlayable playable, Queue<IFile> queue, int index) {
            if (queue.Count == 0) return null;
            return queue.Peek();
        }
    }
}
