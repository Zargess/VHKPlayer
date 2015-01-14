using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Strategies.Selection.Players {
    public class StatSelectionStrategy : IFileSelectionStrategy {
        private IFileSelectionStrategy _pictureStrategy;

        public StatSelectionStrategy(IFileSelectionStrategy pictureStrategy) {
            _pictureStrategy = pictureStrategy;
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            var res = new Queue<IFile>();

            var pic = playable.Content.SingleOrDefault(x => Settings.PlayerStatPictureFolder.ContainsFile(x));
            var vid = playable.Content.SingleOrDefault(x => Settings.PlayerStatVideoFolder.ContainsFile(x));
            var mus = playable.Content.SingleOrDefault(x => Settings.PlayerStatMusicFolder.ContainsFile(x));

            if (mus != null) res.Enqueue(mus);

            // TODO : Consider moving this into a strategy
            if (vid == null) {
                vid = _pictureStrategy.SelectFiles(playable, type).Dequeue();
            }
            res.Enqueue(vid);

            res.Enqueue(pic);

            return res;
        }
    }
}
