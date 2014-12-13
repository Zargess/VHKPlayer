using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.PlayManaging {
    public class PlayManager : IPlayManager {
        public IPlayable CurrentPlayable { get; private set; }
        public Queue<IFile> Queue { get; private set; }
        private PlayType CurrentType { get; set; }
        public IPlayStrategy PlayStrategy { get; private set; }

        public PlayManager(PlayerWindow player, IPlayStrategy playStrategy) {
            PlayStrategy = playStrategy;
        }

        public void PlayQueue() {
            if (Queue == null || Queue.Count == 0) return;
            PlayStrategy.Play(Queue.Dequeue(), CurrentType);
        }

        public void Play(IPlayable playable, PlayType type) {
            Queue = playable.Play(type);
            CurrentType = type;
            PlayQueue();
        }
    }
}