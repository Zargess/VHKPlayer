using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Utility.PlayStrategy
{
    public class SingleFilePlayStrategy : IPlayStrategy
    {
        private bool repeat;
        public bool Repeat
        {
            get
            {
                return repeat;
            }

            set
            {
                repeat = value;
            }
        }

        public FileNode PeekNext(IVideoPlayerController videoPlayer)
        {
            if (videoPlayer.Queue.Count == 0) return null;

            return videoPlayer.Queue.Peek(); 
        }

        public void Play(IEnumerable<FileNode> content, IVideoPlayerController videoPlayer)
        {
            var list = new List<FileNode>(content);
            if (list.Count == 0) return;

            videoPlayer.Play(list[0]);
        }
    }
}
