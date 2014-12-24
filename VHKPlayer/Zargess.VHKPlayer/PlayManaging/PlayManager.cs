using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.EventHandlers;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.PlayManaging {
    public class PlayManager : IPlayManager {
        public IPlayable CurrentPlayable { get; private set; }
        public CustomQueue<IFile> Queue { get; private set; }
        private PlayType CurrentType { get; set; }
        public IPlayStrategy PlayStrategy { get; private set; }
        public IFileSelectionStrategy QueueEmptyStrategy { get; private set; }
        private List<IPlayObserver> Observers { get; set; }
        public IPlayList Auto10SekPlayList { get; private set; }

        public PlayManager(IPlayManagerFactory factory) {
            PlayStrategy = factory.CreatePlayStrategy();
            Queue = factory.CreateQueue();
            Observers = factory.CreateObserverList();
            App.ConfigService.PropertyChanged += (sender, ee) => Auto10SekPlayList = factory.CreateAuto10SekPlayList();
            Auto10SekPlayList = factory.CreateAuto10SekPlayList();
            QueueEmptyStrategy = factory.CreateQueueEmptyStrategy();
        }

        public void PlayQueue() {
            var emptyCaseQueue = QueueEmptyStrategy.SelectFiles(CurrentPlayable, CurrentType);
            if (emptyCaseQueue != null) Queue.SetQueue(emptyCaseQueue);
            if (Queue.Count == 0) return;
            var file = Queue.Dequeue();
            PlayStrategy.Play(file, CurrentType);
            var next = CurrentPlayable.SelectionStrategy.HintNext(Queue, CurrentPlayable, CurrentType);
            if (file.Type != FileType.Music || next.Type == FileType.Music) return;
            PlayStrategy.Play(Queue.Dequeue(), CurrentType);
        }

        public void Play(IPlayable playable, PlayType type) {
            Queue.SetQueue(playable.Play(type));
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