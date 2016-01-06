using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models.Interfaces
{
    public interface IPlayable
    {
        string Name { get; }
        ICollection<FileNode> Content { get; }
        void Play(IPlayStrategy strategy, IVideoPlayerController videoPlayer);
    }
}
