using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayList : IPlayable {
        bool HasAudio { get; }
        void Add(IFile p);
    }
}
