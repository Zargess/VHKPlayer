using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayManager {
        Queue<IPlayable> Queue { get; }

        void PlayQueue();
        void Play(IPlayable playable, PlayType type);
    }
}
