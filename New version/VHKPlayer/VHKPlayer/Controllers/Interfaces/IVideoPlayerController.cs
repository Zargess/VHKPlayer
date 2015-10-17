using System.Collections.Generic;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Controllers.Interfaces
{
    public interface IVideoPlayerController
    {
        bool AutoPlayList { get; set; }
        Queue<FileNode> Queue { get; }

        void AddObserver(IPlayController observer);
        FileNode HintNext();
        void Mute(FileType type);
        void Pause(FileType type);
        void Play(FileNode file);
        void Play(IPlayable playable, IPlayStrategy strategy);
        void PlayQueue();
        void Resume(FileType type);
        void Stop(FileType type);
        void Shutdown();
        void ShowStats();
    }
}
