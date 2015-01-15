using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Selection.PlayLists {
    public class AllFilesSelectionStrategy : IFileSelectionStrategy {
        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            var res = new Queue<IFile>();
            foreach (var file in playable.Content) {
                res.Enqueue(file);
            }
            return res;
        }
    }
}
