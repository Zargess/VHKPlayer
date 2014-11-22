using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Selection {
    public class IteratedFileSelectionStrategy : IFileSelectionStrategy {
        private int Index { get; set; }

        public IteratedFileSelectionStrategy() {
            Index = 0;
        }

        public Queue<IFile> SelectFiles(IPlayable playable) {
            if (playable.Size == 0) return new Queue<IFile>();
            if (Index >= playable.Size) Index = 0;
            var res = new Queue<IFile>();
            res.Enqueue(playable.GetContent()[Index]);
            Index++;
            return res;
        }
    }
}
