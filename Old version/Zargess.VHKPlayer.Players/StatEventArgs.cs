using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Players {
    public class StatEventArgs : EventArgs {
        public Statistics Stats { get; private set; }

        public StatEventArgs(Statistics s) {
            Stats = s;
        }
    }
}
