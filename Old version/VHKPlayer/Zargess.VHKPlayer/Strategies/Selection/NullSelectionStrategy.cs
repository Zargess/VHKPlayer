using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Selection {
    public class NullSelectionStrategy : IFileSelectionStrategy {
        public IFile HintNext(Queue<IFile> q, IPlayable p, PlayType type) {
            return null;
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            return null;
        }
    }
}
