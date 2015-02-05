using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interfaces {
    public interface IPeekStrategy {
        IFile Peek(IPlayable playable, Queue<IFile> queue, int index);
    }
}
