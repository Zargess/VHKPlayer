using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Strategies.Playing {
    public class AutoPlayListPlayStrategy : IPlayStrategy {
        public void Play(IVideoPlayer videoplayer, Queue<IFile> queue, IPlayable playable, PlayType type) {
            var autolist = Settings.AutoPlayList.Play(PlayType.PlayList).ToList();
            autolist.ForEach(x => queue.Enqueue(x));
            videoplayer.PlayQueue();
        }
    }
}
