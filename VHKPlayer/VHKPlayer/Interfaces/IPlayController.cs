using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;

namespace VHKPlayer.Interfaces {
    public interface IPlayController {
        void Play(IFile file);
        void Resume(FileType type);
        void Stop(FileType type);
        void Pause(FileType type);
        void Mute(FileType type);
        void ShowStats(IPlayer player);
        void Update(IFile file);
    }
}