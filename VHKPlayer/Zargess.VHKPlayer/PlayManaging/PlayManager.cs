using System;
using System.Collections.Generic;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Strategies.Playing;

namespace Zargess.VHKPlayer.PlayManaging {
    public class PlayManager : IPlayManager {
        private PlayType _currentType;
        private List<IPlayObserver> _observers;
        private IFileSelectionStrategy _emptyQueueStrategy;
        private IPlayStrategy _playStrategy;
        public IPlayable CurrentPlayable { get; set; }
        public CustomQueue<IFile> Queue { get; private set; }
        public IPlayList Auto10SekPlayList { get; private set; }
        public bool PlayingMusic { get; set; }

        public PlayManager(IPlayManagerFactory factory) {
            _playStrategy = factory.CreatePlayStrategy();
            Queue = factory.CreateQueue();
            _observers = factory.CreateObserverList();
            App.ConfigService.PropertyChanged += (sender, ee) => Auto10SekPlayList = factory.CreateAuto10SekPlayList();
            Auto10SekPlayList = factory.CreateAuto10SekPlayList();
            _emptyQueueStrategy = factory.CreateQueueEmptyStrategy();
        }

        public void PlayQueue() {
            var emptyCaseQueue = _emptyQueueStrategy.SelectFiles(CurrentPlayable, _currentType);
            if (emptyCaseQueue != null) Queue.SetQueue(emptyCaseQueue);
            if (Queue.Count == 0) return;
            var file = Queue.Dequeue();
            _playStrategy.Play(file, _currentType);
            var next = CurrentPlayable.SelectionStrategy.HintNext(Queue, CurrentPlayable, _currentType);
            if (file.Type != FileType.Music || next.Type == FileType.Music) return;
            _playStrategy.Play(Queue.Dequeue(), _currentType);
        }

        public void Play(IPlayable playable, PlayType type) {
            if (type == PlayType.Music) {
                PlayMusic(playable);
            } else {
                PlayPlayerStatStrategy.CancelTimer();
                Queue.SetQueue(playable.Play(type));
                CurrentPlayable = playable;
                _currentType = type;
                PlayQueue();
            }
        }

        // TODO : Make it so that this method mutes video. Consider making this a state pattern so that the might config iof they want this
        private void PlayMusic(IPlayable playable) {
            var files = playable.Play(PlayType.Music);
            if (files.Count != 1) return;
            SetCurrentFile(files.Dequeue());
            PlayingMusic = true;
            Play(FileType.Music);
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