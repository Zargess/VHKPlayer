﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.Strategies.Selection.IPlayers {
    public class PictureSelectionStrategy : IFileSelectionStrategy {
        public IQueuePeekStrategy PeekStrategy { get; private set; }

        public PictureSelectionStrategy(IQueuePeekStrategy peekStrategy) {
            PeekStrategy = peekStrategy;
        }

        public IFile HintNext(Queue<IFile> q, IPlayable p, PlayType type) {
            return PeekStrategy.HintNext(q, 0, p);
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            var res = new Queue<IFile>();
            var file = new FileNode(App.ConfigService.GetPathString("playerFolders", 0));
            res.Enqueue(playable.Content[0]);
            return res;
        }
    }
}
