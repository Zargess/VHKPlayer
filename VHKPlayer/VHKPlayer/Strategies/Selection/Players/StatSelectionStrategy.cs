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
        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            var res = new Queue<IFile>();

            var pic = playable.Content.SingleOrDefault(x => Settings.PlayerStatPictureFolder.ContainsFile(x));
            var vid = playable.Content.SingleOrDefault(x => Settings.PlayerStatVideoFolder.ContainsFile(x));
            var mus = playable.Content.SingleOrDefault(x => Settings.PlayerStatMusicFolder.ContainsFile(x));

            res.Enqueue(pic);
            if (vid != null) res.Enqueue(vid);
            if (mus != null) res.Enqueue(mus);

            return res;
        }
    }
}
