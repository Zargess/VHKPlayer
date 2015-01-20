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
        public CustomQueue<IFile> Queue { get; private set; }

        public VideoPlayer(ISettings folderSetting) {
            Settings.FolderConfig = new GlobalConfigService(folderSetting);
            _controllers = new List<IPlayController>();
            Queue = new CustomQueue<IFile>();
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
            _controllers.ForEach(x => x.Play(FileType.Music));
        }

        public void PlayPlayable(IPlayable playable, PlayType type) {
            var queue = playable.Play(type);
            if (queue.Count == 0) return;

            Play(queue.Dequeue());

            if (queue.Count == 0) return;
            if (type == PlayType.Music) return;

            Queue.SetQueue(queue);
        }

        public void PlayQueue() {

        }

        public void Resume(FileType type) {
            _controllers.ForEach(x => x.Resume(type));
        }

        public void Stop(FileType type) {
            _controllers.ForEach(x => x.Stop(type));
        }
    }
}