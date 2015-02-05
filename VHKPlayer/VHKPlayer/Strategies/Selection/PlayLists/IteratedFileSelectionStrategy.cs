using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Selection.PlayLists {
    public class IteratedFileSelectionStrategy : IFileSelectionStrategy {
        private int _index;
        private IPeekStrategy _peekStrategy;

        public IteratedFileSelectionStrategy(IPeekStrategy peekstrategy) {
            _index = 0;
            _peekStrategy = peekstrategy;
        }

        public IFile HintNext(IPlayable playable, Queue<IFile> queue) {
            return _peekStrategy.Peek(playable, queue, _index);
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            var res = new Queue<IFile>();
            if (playable.Content.Count == 0) return res;
            if (_index >= playable.Content.Count) _index = 0;
            res.Enqueue(playable.Content[_index]);
            _index++;
            return res;
        }
    }
}
