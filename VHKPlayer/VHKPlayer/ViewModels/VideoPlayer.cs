using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Collections;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.ViewModels {
    public class VideoPlayer : IVideoPlayer {
        private List<IPlayController> _controllers;
        private PlayType _currentType;
        private IPlayStrategy _playStrategy;
        private IPlayable _currentPlayable;

        public CustomQueue<IFile> Queue { get; private set; }

        public VideoPlayer(ISettings folderSetting, IPlayStrategy playstrategy) {
            Settings.FolderConfig = new GlobalConfigService(folderSetting);
            _controllers = new List<IPlayController>();
            Queue = new CustomQueue<IFile>();
            _playStrategy = playstrategy;
        }

        public void AddObserver(IPlayController observer) {
            _controllers.Add(observer);
        }

        public void Mute(FileType type) {
            _controllers.ForEach(x => x.Mute(type));
        }

        public void Pause(FileType type) {
            _controllers.ForEach(x => x.Pause(type));
        }

        public void Play(IFile file) {
            _controllers.ForEach(x => x.Update(file));
            _controllers.ForEach(x => x.Play(file.Type));
        }

        public void Play(IPlayable playable, PlayType type) {
            var queue = playable.Play(type);
            if (queue.Count == 0) return;
            _currentType = type;
            _currentPlayable = playable;
            _playStrategy.Play(this, queue, playable, type);

            if (type == PlayType.Music) return;

            Queue.SetQueue(queue);
        }

        public void PlayQueue() {
            _playStrategy.Play(this, Queue, _currentPlayable, _currentType);
        }

        public void Resume(FileType type) {
            _controllers.ForEach(x => x.Resume(type));
        }

        public void Stop(FileType type) {
            _controllers.ForEach(x => x.Stop(type));
        }

        public void Shutdown() {
            foreach (var timer in Settings.Timers) {
                timer.Enabled = false;
                timer.Stop();
            }
        }

        public void ShowStats() {
            if (!(_currentPlayable is IPlayer)) return;
            var player = (IPlayer)_currentPlayable;
            _controllers.ForEach(x => x.ShowStats(player));
        }

        // TODO : Test this code
        public IFile HintNext() {
            if (_currentPlayable == null) return null;
            var next = _currentPlayable.HintNext(Queue);
            var enabled = App.ViewModel.AutoPlayListEnabled;

            if (next == null && enabled) return Settings.AutoPlayList.HintNext(Queue);
            else return _currentPlayable.HintNext(Queue);
        }
    }
}