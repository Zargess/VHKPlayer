using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Selection.IPlayManager {
    public class RepeatSelectionStrategy : IFileSelectionStrategy {
        public IFile HintNext(Queue<IFile> q, IPlayable p, PlayType type) {
            return p.SelectionStrategy.HintNext(q, p, type);
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            return playable.Play(type);
        }
    }
}
