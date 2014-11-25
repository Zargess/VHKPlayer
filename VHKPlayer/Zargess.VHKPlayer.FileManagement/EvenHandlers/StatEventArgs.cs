using System;
using Zargess.VHKPlayer.FileManagement.DataTypes;

namespace Zargess.VHKPlayer.FileManagement.EventHandlers {
    public class StatEventArgs : EventArgs {
        public Statistics Stats { get; private set; }

        public StatEventArgs(Statistics s) {
            Stats = s;
        }
    }
}