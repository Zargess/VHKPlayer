using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Controllers.Interfaces
{
    public interface IVideoPlayer
    {
        Queue<FileNode> Queue { get; }

        void AddObserver(IPlayController observer);
        void Mute(FileType type);
        void Pause(FileType type);
        void Play(IPlayable playable, IPlayStrategy strategy);
        void PlayQueue();
        void Resume(FileType type);
        void Stop(FileType type);
        void Shutdown();
        void ShowStats();
        FileNode HintNext();
    }
}
