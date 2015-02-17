using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Playing {
    public class DoNothingPlayStrategy : IPlayStrategy {
        public void Play(IVideoPlayer videoplayer, Queue<IFile> queue, IPlayable playable, PlayType type) { }
    }
}
