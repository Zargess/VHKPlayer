using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Selection.IPlayLists {
    public class IteratedFileSelectionStrategy : IFileSelectionStrategy {
        private int Index { get; set; }
        public IQueuePeekStrategy PeekStrategy { get; private set; }

        public IteratedFileSelectionStrategy(IQueuePeekStrategy peekStrategy) {
            Index = 0;
            PeekStrategy = peekStrategy;
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            if (playable.Content.Count == 0) return new Queue<IFile>();
            if (Index >= playable.Content.Count) Index = 0;
            var res = new Queue<IFile>();
            res.Enqueue(playable.Content[Index]);
            Index++;
            return res;
        }

        public IFile HintNext(Queue<IFile> q, IPlayable p, PlayType type) {
            return PeekStrategy.HintNext(q, Index, p);
        }
    }
}
