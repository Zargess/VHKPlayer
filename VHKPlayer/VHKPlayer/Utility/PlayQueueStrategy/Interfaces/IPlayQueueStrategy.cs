using System.Collections.Generic;
using VHKPlayer.Controllers;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Utility.PlayQueueStrategy.Interfaces
{
    public interface IPlayQueueStrategy
    {
        void PlayNextItem(Queue<FileNode> queue, VideoPlayerController controller, IPlayStrategy strategy,
            IPlayable previous);
    }
}