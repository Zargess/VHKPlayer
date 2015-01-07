using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayObserver {
        ISoundStrategy SoundStrategy { get; }
        void Play(FileType type);
        void Pause(FileType type);
        void Stop(FileType type);
        void Mute(FileType type);
        void Resume(FileType type);
        void SetCurrentFile(IFile file);
        void ShowStats(IPlayer currentPlayable);
    }
}
