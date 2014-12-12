using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Exceptions;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
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

            var statPicFolder = new FolderNode(PathHandler.AbsolutePath(App.ConfigService.GetPathString("playerFolders", 2)));
            var statMusicFolder = new FolderNode(PathHandler.AbsolutePath(App.ConfigService.GetPathString("playerFolders", 3)));
            var statVideoFolder = new FolderNode(PathHandler.AbsolutePath(App.ConfigService.GetPathString("playerFolders", 4)));

            var pic = content.FirstOrDefault(x => statPicFolder.ContainsFile(x));
            var mus = content.FirstOrDefault(x => statMusicFolder.ContainsFile(x));
            var vid = content.FirstOrDefault(x => statVideoFolder.ContainsFile(x));
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
