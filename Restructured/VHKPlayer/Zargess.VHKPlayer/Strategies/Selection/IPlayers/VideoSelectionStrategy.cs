using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Strategies.Selection.IPlayers {
    public class VideoSelectionStrategy : IFileSelectionStrategy {
        public IFileSelectionStrategy PicSelection { get; private set; }

        public VideoSelectionStrategy(IFileSelectionStrategy picSelection) {
            PicSelection = picSelection;
        }

        public Queue<IFile> SelectFiles(IPlayable playable) {
            var res = new Queue<IFile>();
            var temp = App.ConfigService.GetPathString("playerFolders", 1);
            var vidFolderPath = PathHandler.AbsolutePath(temp);
            if (!playable.Content.Any(x => x.FullPath.ToLower().Contains(vidFolderPath))) return PicSelection.SelectFiles(playable);
            res.Enqueue(playable.Content.First(x => x.FullPath.ToLower().Contains(vidFolderPath)));
            return res;
        }
    }
}
