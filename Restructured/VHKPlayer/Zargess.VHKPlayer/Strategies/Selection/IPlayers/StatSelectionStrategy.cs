using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Exceptions;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Strategies.Selection.IPlayers {
    public class StatSelectionStrategy : IFileSelectionStrategy {

        public IFileSelectionStrategy VidSelection { get; private set; }
        public IFileSelectionStrategy PicSelection { get; private set; }

        public StatSelectionStrategy(IFileSelectionStrategy picSelection, IFileSelectionStrategy vidSelection) {
            PicSelection = picSelection;
            VidSelection = vidSelection;
        }

        public Queue<IFile> SelectFiles(IPlayable playable) {
            // TODO : Make a better way to handle missing files.
            var res = new Queue<IFile>();
            var content = playable.Content;

            string statPicFolder = PathHandler.AbsolutePath(App.ConfigService.GetPathString("playerFolders", 2)).ToLower();
            string statMusicFolder = PathHandler.AbsolutePath(App.ConfigService.GetPathString("playerFolders", 3)).ToLower();
            string statVideoFolder = PathHandler.AbsolutePath(App.ConfigService.GetPathString("playerFolders", 4)).ToLower();
            if (content.Count == 1) return PicSelection.SelectFiles(playable);
            if (!content.Any(x => x.FullPath.ToLower().Contains(statPicFolder))) return VidSelection.SelectFiles(playable);

            // TODO : This might not work
            var pic = content.FirstOrDefault(x => x.FullPath.ToLower().Contains(statPicFolder));
            var mus = content.FirstOrDefault(x => x.FullPath.ToLower().Contains(statMusicFolder));
            var vid = content.FirstOrDefault(x => x.FullPath.ToLower().Contains(statVideoFolder));
            res.Enqueue(pic.Clone());
            if (mus != null) res.Enqueue(mus.Clone());
            if (vid == null) {
                vid = PicSelection.SelectFiles(playable).Dequeue();
            }
            res.Enqueue(vid.Clone());

            return res;
        }
    }
}
