using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interfaces {
    public interface IPeekStrategy {
        IFile HintNext(Queue<IFile> queue, int index);
    }
}
