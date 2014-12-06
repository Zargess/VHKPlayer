using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Selection.IPlayLists {
    public class AllFilesSelectionStrategy : IFileSelectionStrategy {
        public Queue<IFile> SelectFiles(IPlayable playable) {
            var res = new Queue<IFile>();
            foreach (var file in playable.Content) {
                res.Enqueue(file.Clone());
            }
            return res;
        }
    }
}
