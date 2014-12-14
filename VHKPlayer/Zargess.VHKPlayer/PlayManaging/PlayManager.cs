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
        public IFileSelectionStrategy QueueEmptyStrategy { get; private set; }

        public PlayManager(IPlayStrategy playStrategy) {
            PlayStrategy = playStrategy;
        }

        public void PlayQueue() {
            if (CurrentPlayable.Repeat && Queue.Count == 0) {
                Queue = CurrentPlayable.Play(CurrentType);
            }
            if (Queue == null) return;
            if (Queue.Count == 0) return; // TODO : Handle Auto 10 sek
            PlayStrategy.Play(Queue.Dequeue(), CurrentType);
        }

        public void Play(IPlayable playable, PlayType type) {
            Queue = playable.Play(type);
            CurrentPlayable = playable;
            CurrentType = type;
            PlayQueue();
        }
    }
}