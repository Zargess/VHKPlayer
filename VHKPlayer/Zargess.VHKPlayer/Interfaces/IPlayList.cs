using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayList : IPlayable {
        void Add(IFile p);
    }
}
