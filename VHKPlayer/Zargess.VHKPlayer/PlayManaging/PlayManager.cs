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

        public event PlayerFunctionHandler PlayFunction;
        public event PlayerFunctionHandler PauseFunction;
        public event PlayerFunctionHandler StopFunction;
        public event PlayerFunctionHandler MuteFunction;
        public event PlayerFunctionHandler ResumeFunction;

        public PlayManager(IPlayStrategy playStrategy) {
            PlayStrategy = playStrategy;
        }

        public void PlayQueue() {
            if (Queue == null) return;
            if (CurrentPlayable.Repeat && Queue.Count == 0) {
                Queue = CurrentPlayable.Play(CurrentType);
            }
            if (Queue.Count == 0) return; // TODO : Handle Auto 10 sek
            PlayStrategy.Play(Queue.Dequeue(), CurrentType);
            
        }

        public void Play(IPlayable playable, PlayType type) {
            Queue = playable.Play(type);
            CurrentPlayable = playable;
            CurrentType = type;
            PlayQueue();
        }

        public void Play(FileType type) {
            RaisePlayerFunction(PlayFunction, PlayerFunctionType.Play, GetCurrentFile(type));
        }

        public void Pause(FileType type) {
            RaisePlayerFunction(PauseFunction, PlayerFunctionType.Pause, GetCurrentFile(type));
        }

        public void Stop(FileType type) {
            RaisePlayerFunction(StopFunction, PlayerFunctionType.Stop, GetCurrentFile(type));
        }

        public void Mute(FileType type) {
            RaisePlayerFunction(MuteFunction, PlayerFunctionType.Mute, GetCurrentFile(type));
        }

        public void Resume(FileType type) {
            RaisePlayerFunction(ResumeFunction, PlayerFunctionType.Resume, GetCurrentFile(type));
        }

        /// <summary>
        /// Finds the wanted File from the currently active files from each type.
        /// Precondition: Parameter must be either Video, Music or Picture
        /// </summary>
        /// <param name="type">The wanted file type</param>
        /// <returns>The wanted file</returns>
        private IFile GetCurrentFile(FileType type) {
            IFile res = null;
            switch (type) {
                case FileType.Music:
                    res = App.PlayerViewModel.CurrentMusicFile;
                    break;
                case FileType.Video:
                    res = App.PlayerViewModel.CurrentVideoFile;
                    break;
                case FileType.Picture:
                    res = App.PlayerViewModel.CurrentPictureFile;
                    break;
            }
            return res;
        }

        private void RaisePlayerFunction(PlayerFunctionHandler handler, PlayerFunctionType type, IFile file) {
            if (handler == null) return;
            handler(this, new PlayerFunctionEventArgs(type, file));
        }
    }
}