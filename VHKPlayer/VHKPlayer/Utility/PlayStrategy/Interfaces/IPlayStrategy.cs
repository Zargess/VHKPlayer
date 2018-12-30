using System.Collections.Generic;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Utility.PlayStrategy.Interfaces
{
    /// <summary>
    /// Play Strategy descriping how a playable should be played. Must have either none or an empty constructor.
    /// </summary>
    public interface IPlayStrategy
    {
        bool Repeat { get; set; }
        void Play(IEnumerable<FileNode> content, IVideoPlayerController videoPlayer);
        FileNode PeekNext(IVideoPlayerController videoPlayer);
    }
}