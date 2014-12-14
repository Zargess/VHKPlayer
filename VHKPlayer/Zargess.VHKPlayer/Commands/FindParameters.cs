using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Commands {
    public class FindParameters {
        public IPlayable Playable { get; set; }
        public string ControlName { get; set; }
    }
}
