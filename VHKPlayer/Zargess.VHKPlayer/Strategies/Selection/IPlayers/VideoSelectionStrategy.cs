using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Strategies.Selection.IPlayers {
    public class VideoSelectionStrategy : IFileSelectionStrategy {
        public IFileSelectionStrategy PicSelection { get; private set; }
        public IQueuePeekStrategy PeekStrategy { get; private set; }

        public VideoSelectionStrategy(IFileSelectionStrategy picSelection, IQueuePeekStrategy peekStrategy) {
            PicSelection = picSelection;
            PeekStrategy = peekStrategy;
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            var res = new Queue<IFile>();
            var temp = App.ConfigService.GetPathString("playerFolders", 1);
            var vidFolderPath = new FolderNode(PathHandler.AbsolutePath(temp).ToLower());
            vidFolderPath.StopWatcher();
            res.Enqueue(playable.Content.First(x => vidFolderPath.ContainsFile(x)));
            return res;
        }

        public IFile HintNext(Queue<IFile> q, IPlayable p, PlayType type) {
            return PeekStrategy.HintNext(q, 0, p);
        }
    }
}
