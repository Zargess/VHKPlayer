using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Selection.ISingleItemPlayables {
    public class SingleItemPlayableSelectionStrategy : IFileSelectionStrategy {
        public IFile HintNext(Queue<IFile> q, IPlayable p, PlayType type) {
            return null;
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            var res = new Queue<IFile>();
            if (playable.Content.Count > 0) res.Enqueue(playable.Content[0]);
            return res;
        }
    }
}
