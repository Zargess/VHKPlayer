﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Strategies.Selection.Players {
    public class VideoSelectionStrategy : IFileSelectionStrategy {
        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            var res = new Queue<IFile>();

            var file = playable.Content.SingleOrDefault(x => Settings.PlayerVideoFolder.ContainsFile(x));
            if (file == null) return res;

            res.Enqueue(file);

            return res;
        }
    }
}
