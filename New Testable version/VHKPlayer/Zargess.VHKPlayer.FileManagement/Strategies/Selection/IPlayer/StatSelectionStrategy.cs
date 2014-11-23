using System.Linq;
using System.Collections.Generic;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Selection.IPlayer {
    public class StatSelectionStrategy : IFileSelectionStrategy {

        public IFileSelectionStrategy VidSelection { get; private set; }

        public StatSelectionStrategy(IFileSelectionStrategy vidSelection) {
            VidSelection = vidSelection;
        }

        public Queue<IFile> SelectFiles(IPlayable playable) {
            var res = new Queue<IFile>();
            var content = playable.GetContent();

            string statFolder = PathHandler.AbsolutePath(@"root\spillervideostat");
            string statMusicFolder = PathHandler.AbsolutePath(@"root\spillervideostat\mp3");
            string statVideoFolder = PathHandler.AbsolutePath(@"root\spillervideostat\video");
            if (!content.Any(x => x.FullPath.ToLower().Contains(statFolder))) return VidSelection.SelectFiles(playable);

            var pic = content.SingleOrDefault(x => x.FullPath.ToLower().Contains(statFolder));
            var mus = content.SingleOrDefault(x => x.FullPath.ToLower().Contains(statMusicFolder));
            var vid = content.SingleOrDefault(x => x.FullPath.ToLower().Contains(statVideoFolder));
            res.Enqueue(pic);
            res.Enqueue(mus);
            res.Enqueue(vid);

            return res;
        }
    }
}