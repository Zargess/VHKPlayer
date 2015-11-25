using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Utility.HandleStatFile.Interfaces
{
    public interface IHandleStatFileStrategy
    {
        void HandleFile(IVideoPlayerController controller, FileNode file);
        void StopTimer();
    }
}
