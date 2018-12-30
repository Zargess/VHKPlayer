using System.Collections.Generic;
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

        public FileNode PeekNext(IVideoPlayerController videoPlayer)
        {
            if (videoPlayer.Queue.IsEmpty()) return null;
            return videoPlayer.Queue.Peek();
        }

        public void Play(IEnumerable<FileNode> content, IVideoPlayerController videoPlayer)
        {
            videoPlayer.Queue.SetQueue(content);
            videoPlayer.PlayQueue();
        }
    }
}