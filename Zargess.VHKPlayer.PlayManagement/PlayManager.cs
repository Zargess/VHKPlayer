using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Zargess.VHKPlayer.PlayManagement {
    public class PlayManager {
        public MediaElement Video { get; private set; }
        public MediaElement Audio { get; private set; }
        // TODO : Implement different play functions, a video and a audio queue
        public PlayManager(MediaElement vid, MediaElement aud) {
            Video = vid;
            Audio = aud;
        }
    }
}
