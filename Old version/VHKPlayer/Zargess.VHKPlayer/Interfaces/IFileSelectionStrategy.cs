using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IFileSelectionStrategy {
        Queue<IFile> SelectFiles(IPlayable playable, PlayType type);
        IFile HintNext(Queue<IFile> q, IPlayable p, PlayType type);
    }
}
