using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.EventHandlers;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.PlayManaging {
    public class PlayManager : IPlayManager {
        public IPlayable CurrentPlayable { get; private set; }
        public Queue<IFile> Queue { get; private set; }
        private PlayType CurrentType { get; set; }
        public IPlayStrategy PlayStrategy { get; private set; }
        public IFileSelectionStrategy QueueEmptyStrategy { get; private set; }
        private List<IPlayObserver> Observers { get; set; }

        public PlayManager(IPlayStrategy playStrategy) {
            PlayStrategy = playStrategy;
            Observers = new List<IPlayObserver>();
        }

        // TODO : Handle Music Queue
        public void PlayQueue() {
            if (Queue == null) return;
            if (CurrentPlayable.Repeat && Queue.Count == 0) {
                Queue = CurrentPlayable.Play(CurrentType);
            }
            if (Queue.Count == 0) return; // TODO : Handle Auto 10 sek
            var file = Queue.Dequeue();
            PlayStrategy.Play(file, CurrentType);
            if (file.Type != FileType.Music || CurrentPlayable.SelectionStrategy.HintNext(Queue, CurrentPlayable, CurrentType).Type == FileType.Music) return;
            PlayStrategy.Play(Queue.Dequeue(), CurrentType);
        }

        public void Play(IPlayable playable, PlayType type) {
            Queue = playable.Play(type);
            CurrentPlayable = playable;
            CurrentType = type;
            PlayQueue();
        }

        public void AddObserver(IPlayObserver observer) {
            Observers.Add(observer);
        }

        public void SetCurrentFile(IFile file) {
            Observers.ForEach(x => x.SetCurrentFile(file));
        }

        public void Play(FileType type) {
            Observers.ForEach(x => x.Play(type));
        }

        public void Pause(FileType type) {
            Observers.ForEach(x => x.Pause(type));
        }

        public void Stop(FileType type) {
            Observers.ForEach(x => x.Stop(type));
        }

        public void Mute(FileType type) {
            Observers.ForEach(x => x.Mute(type));
        }

        public void Resume(FileType type) {
            Observers.ForEach(x => x.Resume(type));
        }
    }
}