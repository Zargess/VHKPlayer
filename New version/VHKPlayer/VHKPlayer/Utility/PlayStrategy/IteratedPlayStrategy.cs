using System.Collections.Generic;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Utility.PlayStrategy
{
    public class IteratedPlayStrategy : IPlayStrategy
    {
        private int counter;
        private List<FileNode> content;

        public bool Repeat { get; set; }

        public FileNode PeekNext(IVideoPlayer videoPlayer)
        {
            if (content == null) return null;
            if (counter >= content.Count) counter = 0;
            return content[counter];
        }

        public void Play(IEnumerable<FileNode> content, IVideoPlayer videoPlayer)
        {
            this.content = new List<FileNode>(content);

            var file = PeekNext(videoPlayer);
            counter++;

            videoPlayer.Queue.SetQueue(new List<FileNode>() { file });
            videoPlayer.PlayQueue();
        }
    }
}