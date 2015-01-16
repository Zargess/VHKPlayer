using System;
using System.Collections.Generic;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Factories.ISingleItemPlayables;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Strategies.Playing;

namespace Zargess.VHKPlayer.PlayManaging {
    public class PlayManager : IPlayManager {
        private List<IPlayObserver> _observers;
        private IFileSelectionStrategy _emptyQueueStrategy;
        private IPlayFileStrategy _playFileStrategy;
        private IPlayablePlayStrategy _playPlayableStrategy;
        private IPlayManagerFactory _factory;
        public IPlayable CurrentPlayable { get; set; }
        public CustomQueue<IFile> Queue { get; private set; } // TODO : Consider making a Queue for music
        public IPlayList Auto10SekPlayList { get; private set; }
        public bool PlayingMusic { get; set; }
        public PlayType CurrentType { get; set; }

        public PlayManager(IPlayManagerFactory factory) {
            _factory = factory;
            _playFileStrategy = _factory.CreatePlayFileStrategy();
            Queue = _factory.CreateQueue();
            _observers = _factory.CreateObserverList();
            App.ConfigService.PropertyChanged += (sender, ee) => Auto10SekPlayList = _factory.CreateAuto10SekPlayList();
            Auto10SekPlayList = _factory.CreateAuto10SekPlayList();
            _emptyQueueStrategy = _factory.CreateQueueEmptyStrategy();
            _playPlayableStrategy = _factory.CreatePlayablePlayStrategy();
        }

        // TODO : Consider move out of here or simplify
        public void PlayQueue() {
            var emptyCaseQueue = _emptyQueueStrategy.SelectFiles(CurrentPlayable, CurrentType);
            if (emptyCaseQueue != null) Queue.SetQueue(emptyCaseQueue);
            if (Queue.Count == 0) return;
            var file = Queue.Dequeue();
            _playFileStrategy.Play(file, CurrentType);
            var next = CurrentPlayable.SelectionStrategy.HintNext(Queue, CurrentPlayable, CurrentType);
            if (file.Type != FileType.Music || next.Type == FileType.Music) return;
            _playFileStrategy.Play(Queue.Dequeue(), CurrentType);
        }

        public void Play(IPlayable playable, PlayType type) {
            _playPlayableStrategy.Play(this, playable, type);
        }

        public void AddObserver(IPlayObserver observer) {
            _observers.Add(observer);
        }

        public void SetCurrentFile(IFile file) {
            _observers.ForEach(x => x.SetCurrentFile(file));
        }

        public void Play(FileType type) {
            _observers.ForEach(x => x.Play(type));
        }

        public void Pause(FileType type) {
            _observers.ForEach(x => x.Pause(type));
        }

        public void Stop(FileType type) {
            _observers.ForEach(x => x.Stop(type));
        }

        public void Mute(FileType type) {
            _observers.ForEach(x => x.Mute(type));
        }

        public void Resume(FileType type) {
            _observers.ForEach(x => x.Resume(type));
        }

        public void ShowStats() {
            if (!(CurrentPlayable is IPlayer)) return;
            var player = (IPlayer)CurrentPlayable;
            _observers.ForEach(x => x.ShowStats(player));
        }
    }
}