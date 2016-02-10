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
    public class PlayListPlayStrategy : IPlayStrategy
    {
        public bool Repeat { get; set; }

        public FileNode PeekNext(IVideoPlayerController videoPlayer)
        {
            return null;
        }

        public void Play(IEnumerable<FileNode> content, IVideoPlayerController videoPlayer)
        {
            
        }
    }
}
