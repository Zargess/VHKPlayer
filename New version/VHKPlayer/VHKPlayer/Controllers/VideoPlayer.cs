using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Controllers
{
    public class VideoPlayer : IVideoPlayer
    {
        private List<IPlayController> observers;
        private IPlayStrategy videoPlayStrategy;
        private IPlayable previousMusicPlayable, previousVideoPlayable;

        public Queue<FileNode> Queue { get; set; }

        public VideoPlayer()
        {
            observers = new List<IPlayController>();
        }

        public void AddObserver(IPlayController observer)
        {
            observers.Add(observer);
        }

        public FileNode HintNext()
        {
            return videoPlayStrategy.PeekNext();
        }

        public void Mute(FileType type)
        {
            observers.ForEach(x => x.Mute(type));
        }

        public void Pause(FileType type)
        {
            observers.ForEach(x => x.Pause(type));
        }

        // TODO : Consider how Play should work now.
        public void Play(IPlayable playable, IPlayStrategy strategy)
        {
            throw new NotImplementedException();
        }

        public void PlayQueue()
        {
            throw new NotImplementedException();
        }

        public void Resume(FileType type)
        {
            observers.ForEach(x => x.Resume(type));
        }

        public void ShowStats()
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public void Stop(FileType type)
        {
            observers.ForEach(x => x.Stop(type));
        }
    }
}
