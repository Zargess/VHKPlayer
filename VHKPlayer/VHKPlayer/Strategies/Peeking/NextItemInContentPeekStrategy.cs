using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Peeking {
    public class NextItemInContentPeekStrategy : IPeekStrategy {
        public IFile Peek(IPlayable playable, Queue<IFile> queue, int index) {
            if (index >= playable.Content.Count) index = 0;
            return playable.Content[index];
        }
    }
}
