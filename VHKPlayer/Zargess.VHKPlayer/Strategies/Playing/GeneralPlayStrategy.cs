using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Playing {
    public class GeneralPlayStrategy : IPlayFileStrategy {
        private IPlayFileStrategy PlayFileStrategy { get; set; }
        private IPlayFileStrategy PlayPlayerStatStrategy { get; set; }
        private IPlayFileStrategy CurrentStrategy { get; set; }

        public GeneralPlayStrategy(IPlayFileStrategy playFileStrategy, IPlayFileStrategy playPlayerStatStrategy) {
            PlayFileStrategy = playFileStrategy;
            PlayPlayerStatStrategy = playPlayerStatStrategy;
        }

        public void Play(IFile file, PlayType type) {
            if (type != PlayType.PlayerStat) {
                CurrentStrategy = PlayFileStrategy;
            } else {
                CurrentStrategy = PlayPlayerStatStrategy;
            }
            CurrentStrategy.Play(file, type);
        }
    }
}
