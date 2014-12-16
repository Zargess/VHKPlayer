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
        public IQueuePeekStrategy PeekStrategy { get; private set; }

        public StatSelectionStrategy(IFileSelectionStrategy picSelection, IFileSelectionStrategy vidSelection, IQueuePeekStrategy peekStrategy) {
            PicSelection = picSelection;
            VidSelection = vidSelection;
            PeekStrategy = peekStrategy;
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
            res.Enqueue(pic);
            if (mus != null) res.Enqueue(mus);
            if (vid == null) {
                vid = PicSelection.SelectFiles(playable).Dequeue();
            }
            res.Enqueue(vid);

            return res;
        }

        public IFile HintNext(Queue<IFile> q, IPlayable p) {
            return PeekStrategy.HintNext(q, 0, p);
        }
    }
}
