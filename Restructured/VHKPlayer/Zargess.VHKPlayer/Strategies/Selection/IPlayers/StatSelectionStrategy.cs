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
            var res = new Queue<IFile>();
            var content = playable.Content;

            string statFolder = PathHandler.AbsolutePath(App.ConfigService.GetPathString("playerFolders", 2));
            string statMusicFolder = PathHandler.AbsolutePath(App.ConfigService.GetPathString("playerFolders", 3));
            string statVideoFolder = PathHandler.AbsolutePath(App.ConfigService.GetPathString("playerFolders", 4));
            if (content.Count == 1) return PicSelection.SelectFiles(playable);
            if (!content.Any(x => x.FullPath.ToLower().Contains(statFolder))) return VidSelection.SelectFiles(playable);

            var pic = content.FirstOrDefault(x => x.FullPath.ToLower().Contains(statFolder));
            var mus = content.FirstOrDefault(x => x.FullPath.ToLower().Contains(statMusicFolder));
            var vid = content.FirstOrDefault(x => x.FullPath.ToLower().Contains(statVideoFolder));
            if (pic == null || mus == null) throw new FilesMissingException("Player stat files does not meet requirements. There should be atleast one picture and one music file pr. player.");
            res.Enqueue(mus.Clone());
            res.Enqueue(pic.Clone());
            if (vid == null) {
                vid = PicSelection.SelectFiles(playable).Dequeue();
            }
            res.Enqueue(vid.Clone());

            return res;
        }
    }
}
