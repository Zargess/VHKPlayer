using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.EventHandlers {
    public class PlayerFunctionEventArgs : EventArgs {
        public PlayerFunctionType Type { get; private set; }
        public IFile File { get; private set; }

        public PlayerFunctionEventArgs(PlayerFunctionType type, IFile file) {
            Type = type;
            File = file;
        }
    }
}
