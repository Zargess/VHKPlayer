using System;
using System.Collections.Generic;

namespace Zargess.VHKPlayer.FileManagement.Test {
    public class AllFileSelectionStrategy : IFileSelectionStrategy {
        public Queue<IFile> SelectFiles(IPlayable playable) {
            var res = new Queue<IFile>();
            foreach (var file in playable.Content) {
                res.Enqueue(new FileNode(file.FullPath));
            }
            return res;
        }
    }
}