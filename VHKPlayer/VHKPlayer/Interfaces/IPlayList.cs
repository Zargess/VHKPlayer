using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interfaces {
    public interface IPlayList : IPlayable, IFolderObserver {
        bool HasAudio { get; }
    }
}
