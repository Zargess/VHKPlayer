using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Playing {
    public class PlayFileStrategy : IPlayStrategy {
        public void Play(IVideoPlayer videoplayer, Queue<IFile> queue, IPlayable playable, PlayType type) {
            var file = queue.Dequeue();
            videoplayer.Play(file);
            if (file.Type != FileType.Music) return;
            if (queue.Count == 0) return;

            var next = playable.HintNext(queue);
            if (next.Type == FileType.Music) return;

            videoplayer.Play(queue.Dequeue());
        }
    }
}
