using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Collections;
using VHKPlayer.Enums;

namespace VHKPlayer.Interfaces {
    public interface IVideoPlayer {
        CustomQueue<IFile> Queue { get; }
        void PlayQueue();
        void Play(IPlayable playable, PlayType type);
        void AddObserver(IPlayController observer);
        void Play(IFile file);
        void Resume(FileType type);
        void Stop(FileType type);
        void Pause(FileType type);
        void Mute(FileType type);
        void Shutdown();
    }
}