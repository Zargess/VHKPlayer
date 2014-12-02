using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.GUI.UtilClasses {
    public class FindParameters {
        public IPlayable Playable { get; set; }
        public string TabItemName { get; set; }
    }
}
 