using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayablePlayStrategy {
        void Play(IPlayManager manager, IPlayable playable, PlayType type);
    }
}
