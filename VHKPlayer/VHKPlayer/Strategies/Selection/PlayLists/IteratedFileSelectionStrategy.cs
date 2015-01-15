using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Selection.PlayLists {
    public class IteratedFileSelectionStrategy : IFileSelectionStrategy {
        private int _index;

        public IteratedFileSelectionStrategy() {
            _index = 0;
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            var res = new Queue<IFile>();
            if (playable.Content.Count == 0) return res;
            if (_index >= playable.Content.Count) _index = 0;
            res.Enqueue(playable.Content[_index]);
            _index++;
            return res;
        }
    }
}
