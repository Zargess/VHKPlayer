using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Playing {
    public class PlayPlayerStatStrategy : IPlayStrategy {

        public void Play(IVideoPlayer videoplayer, IFile file, PlayType type) {
            videoplayer.Play(file);
        }
    }
}
