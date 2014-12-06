using System;
using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.EventHandlers {
    public class StatEventArgs : EventArgs {
        private Statistics statistics;

        public StatEventArgs(Statistics statistics) {
            this.statistics = statistics;
        }

        public Statistics Stats { get; private set; }
    }
}