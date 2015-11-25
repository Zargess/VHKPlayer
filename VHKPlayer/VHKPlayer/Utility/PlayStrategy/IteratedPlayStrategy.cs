using System.Collections.Generic;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Utility.PlayStrategy
{
    public class IteratedPlayStrategy : IPlayStrategy
    {
        private int _counter;
        private List<FileNode> _content;

        public bool Repeat { get; set; }

        public FileNode PeekNext(IVideoPlayerController videoPlayer)
        {
            if (_content == null) return null;
            if (_counter >= _content.Count) _counter = 0;
            return _content[_counter];
        }

        public void Play(IEnumerable<FileNode> content, IVideoPlayerController videoPlayer)
        {
            this._content = new List<FileNode>(content);

            var file = PeekNext(videoPlayer);
            _counter++;

            videoPlayer.Queue.SetQueue(new List<FileNode>() { file });
            videoPlayer.PlayQueue();
        }
    }
}