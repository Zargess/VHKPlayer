using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Playing.Playable {
    public class ViewablePlayablePlayStrategy : IPlayablePlayStrategy {
        public void Play(IPlayManager manager, IPlayable playable, PlayType type) {
            PlayPlayerStatStrategy.CancelTimer();
            manager.Queue.SetQueue(playable.Play(type));
            manager.CurrentPlayable = playable;
            manager.CurrentType = type;
            manager.PlayQueue();
        }
    }
}
