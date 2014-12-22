using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Playing {
    public class GeneralPlayStrategy : IPlayStrategy {
        private IPlayStrategy PlayFileStrategy { get; set; }
        private IPlayStrategy PlayPlayerStatStrategy { get; set; }
        private IPlayStrategy CurrentStrategy { get; set; }

        public GeneralPlayStrategy(IPlayStrategy playFileStrategy, IPlayStrategy playPlayerStatStrategy) {
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
