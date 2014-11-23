using System;
using System.Collections.Generic;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Selection.IPlayer {
    public class StatSelectionStrategy : IFileSelectionStrategy {
        public Queue<IFile> SelectFiles(IPlayable playable) {
            var res = new Queue<IFile>();
            var content = playable.GetContent();

            

            return res;
        }
    }
}
