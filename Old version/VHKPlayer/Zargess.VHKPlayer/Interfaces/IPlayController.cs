using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.EventHandlers;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayController {
        void Play(FileType type);
        void Pause(FileType type);
        void Stop(FileType type);
        void Mute(FileType type);
        void Resume(FileType type);
    }
}
