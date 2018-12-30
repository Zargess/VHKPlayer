using System.Collections.Generic;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Utility.PlayStrategy.Interfaces
{
    public interface IPlayStrategy
    {
        bool Repeat { get; set; }
        void Play(IEnumerable<FileNode> content, IVideoPlayerController videoPlayer);
        FileNode PeekNext(IVideoPlayerController videoPlayer);
    }
}