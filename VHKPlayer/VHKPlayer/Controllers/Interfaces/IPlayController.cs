using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;

namespace VHKPlayer.Controllers.Interfaces
{
    public interface IPlayController
    {
        void Mute(FileType type);
        void Pause(FileType type);
        void Play(FileNode file);
        void Resume(FileType type);
        void ShowStats(Player p);
        void Stop(FileType type);
    }
}
