using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderFromStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Utility.PlayStrategy
{
    public class PlayerPicturePlayStrategy : IPlayStrategy
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

        public PlayerPicturePlayStrategy()
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
            var folder = processor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerPictureFolderSettingName
            });

            var picture = content.AsParallel().SingleOrDefault(x => folder.Contains(x));

            if (picture == null) return;

            videoPlayer.Queue.SetQueue(new List<FileNode>
            {
                picture
            });
            videoPlayer.PlayQueue();
        }
    }
}
