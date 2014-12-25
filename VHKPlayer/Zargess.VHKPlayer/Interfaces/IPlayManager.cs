using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayManager : IPlayController {
        CustomQueue<IFile> Queue { get; }
        IPlayable CurrentPlayable { get; }
        IPlayList Auto10SekPlayList { get; }

        void PlayQueue();
        void Play(IPlayable playable, PlayType type);
        void SetCurrentFile(IFile file);
        void AddObserver(IPlayObserver observer);
        void ShowStats();
    }
}