using System;
using System.Collections.Generic;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Selection {
    public class AllFilesSelectionStrategy : IFileSelectionStrategy {
        public Queue<IFile> SelectFiles(IPlayable playable) {
            var res = new Queue<IFile>();
            foreach (var file in playable.GetContent()) {
                res.Enqueue(new FileNode(file.FullPath));
            }
            return res;
        }
    }
}