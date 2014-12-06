using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.Strategies.Selection.IPlayers {
    public class PictureSelectionStrategy : IFileSelectionStrategy {
        public Queue<IFile> SelectFiles(IPlayable playable) {
            var res = new Queue<IFile>();
            var file = new FileNode(App.ConfigService.GetPathString("playerFolders", 0));
            res.Enqueue(playable.Content[0]);
            return res;
        }
    }
}
