using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Utility.PlayStrategy
{
    public class PlayerStatPlayStrategy : IPlayStrategy
    {
        private bool repeat;
        private readonly IQueryProcessor processor;

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

        public PlayerStatPlayStrategy()
        {
            processor = App.Container.Resolve<IQueryProcessor>();
        }

        public FileNode PeekNext(IVideoPlayerController videoPlayer)
        {
            if (videoPlayer.Queue.Count == 0) return null;

            return videoPlayer.Queue.Peek();
        }

        public void Play(IEnumerable<FileNode> content, IVideoPlayerController videoPlayer)
        {
            throw new NotImplementedException();
        }
    }
}
