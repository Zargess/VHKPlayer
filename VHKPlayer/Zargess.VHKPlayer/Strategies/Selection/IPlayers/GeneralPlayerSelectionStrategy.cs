using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Selection.IPlayers {
    public class GeneralPlayerSelectionStrategy : IFileSelectionStrategy {
        private IFileSelectionStrategy PicSelectionStrategy { get; set; }
        private IFileSelectionStrategy VidSelectionStrategy { get; set; }
        private IFileSelectionStrategy StatSelectionStrategy { get; set; }
        private IFileSelectionStrategy CurrentStrategy { get; set; }

        public GeneralPlayerSelectionStrategy(IFileSelectionStrategy picSelectionStrategy, IFileSelectionStrategy vidSelectionStrategy, IFileSelectionStrategy statSelectionStrategy) {
            PicSelectionStrategy = picSelectionStrategy;
            VidSelectionStrategy = vidSelectionStrategy;
            StatSelectionStrategy = statSelectionStrategy;
            CurrentStrategy = PicSelectionStrategy;
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            SetCurrentStrategy(type);
            return CurrentStrategy.SelectFiles(playable, type);
        }

        public IFile HintNext(Queue<IFile> q, IPlayable p, PlayType type) {
            SetCurrentStrategy(type);
            return CurrentStrategy.HintNext(q, p, type);
        }

        private void SetCurrentStrategy(PlayType type) {
            if (type == PlayType.PlayerPic) CurrentStrategy = PicSelectionStrategy;
            else if (type == PlayType.PlayerVid) CurrentStrategy = VidSelectionStrategy;
            else if (type == PlayType.PlayerStat) CurrentStrategy = StatSelectionStrategy;
            else throw new InvalidOperationException("Wrong PlayType for players!");
        }
    }
}
