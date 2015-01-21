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
            Play(queue.Dequeue());

            if (type == PlayType.Music) return;

            Queue.SetQueue(queue);
        }

        public void PlayQueue() {
            if (Queue.Count == 0) return;
            var file = Queue.Dequeue();

            _playStrategy.Play(this, file, _currentType);
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
    }
}