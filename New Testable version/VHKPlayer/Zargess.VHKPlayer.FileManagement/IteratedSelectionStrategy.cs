using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class IteratedFileSelectionStrategy : IFileSelectionStrategy {
        private int Index { get; set; }

        public IteratedFileSelectionStrategy() {
            Index = 0;
        }

        public Queue<IFile> SelectFiles(IPlayable playable) {
            if (playable.Content.Count == 0) return new Queue<IFile>();
            if (Index >= playable.Content.Count) Index = 0;
            var res = new Queue<IFile>();
            res.Enqueue(playable.Content[Index]);
            Index++;
            return res;
        }
    }
}
