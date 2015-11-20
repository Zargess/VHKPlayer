using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderFromStringSetting;
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
            var statMusicFolder = processor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerStatMusicFolderSettingName
            });

            var statVideoFolder = processor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerStatVideoFolderSettingName
            });

            var statPictureFolder = processor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerStatPictureFolderSettingName
            });

            var music = content.AsParallel().SingleOrDefault(x => statMusicFolder.Contains(x));
            var video = content.AsParallel().SingleOrDefault(x => statVideoFolder.Contains(x));
            var picture = content.AsParallel().SingleOrDefault(x => statPictureFolder.Contains(x));

            throw new NotImplementedException();
            // TODO : Figure out a way for the video player to know when to start the timer
        }
    }
}
