using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Utility.PlayStrategy
{
    public class AllFilesPlayStrategy : IPlayStrategy
    {
        public bool Done { get; private set; }
        public bool Repeat { get; set; }

        public FileNode PeekNext(IVideoPlayer videoPlayer)
        {
            if (videoPlayer.Queue.IsEmpty()) return null;
            return videoPlayer.Queue.Peek();
        }

        public void Play(IEnumerable<FileNode> content, IVideoPlayer videoPlayer)
        {
            videoPlayer.Queue.SetQueue(content);
            videoPlayer.PlayQueue();
        }
    }
}
