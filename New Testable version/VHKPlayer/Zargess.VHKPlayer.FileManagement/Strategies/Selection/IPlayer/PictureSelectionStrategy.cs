using System.Collections.Generic;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Selection.IPlayer {
    public class PictureSelectionStrategy : IFileSelectionStrategy {
        public Queue<IFile> SelectFiles(IPlayable playable) {
            var res = new Queue<IFile>();
            res.Enqueue(playable.GetContent()[0]);
            return res;
        }
    }
}
