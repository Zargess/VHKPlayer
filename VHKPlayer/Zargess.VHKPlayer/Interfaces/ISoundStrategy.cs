using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface ISoundStrategy {
        // TODO : Think of a good design for this strategy pattern. It needs to change between select the current sound level or fade in and out the sound. Also when faded option is chosen make a thread that manages when the sound should fade out and then apply it.
        void Starting();
        void Stoping();
    }
}