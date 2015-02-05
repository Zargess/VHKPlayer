using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Selection.PlayLists {
    public class AllFilesSelectionStrategy : IFileSelectionStrategy {
        private IPeekStrategy _peekStrategy;

        public AllFilesSelectionStrategy(IPeekStrategy peekstrategy) {
            _peekStrategy = peekstrategy;
        }

        public IFile HintNext(IPlayable playable, Queue<IFile> queue) {
            return _peekStrategy.Peek(playable, queue, 0);
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            var res = new Queue<IFile>();
            foreach (var file in playable.Content) {
                res.Enqueue(file);
            }
            return res;
        }
    }
}
