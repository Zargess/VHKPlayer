using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Utility.PlayStrategy.Interfaces
{
    public interface IPlayStrategy
    {
        bool Repeat { get; }
        bool Done { get; }
        void Play(ICollection content, IVideoPlayer videoPlayer);
        FileNode PeekNext();
    }
}
