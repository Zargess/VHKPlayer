using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetFolderFromStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Utility.PlayStrategy
{
    public class PlayerStatPlayStrategy : IPlayStrategy
    {
        private bool _repeat;
        private readonly IQueryProcessor _processor;

        public bool Repeat
        {
            get { return _repeat; }

            set { _repeat = value; }
        }

        public PlayerStatPlayStrategy()
        {
            _processor = App.Container.Resolve<IQueryProcessor>();
        }

        public FileNode PeekNext(IVideoPlayerController videoPlayer)
        {
            if (videoPlayer.Queue.Count == 0) return null;

            return videoPlayer.Queue.Peek();
        }

        public void Play(IEnumerable<FileNode> content, IVideoPlayerController videoPlayer)
        {
            var statMusicFolder = _processor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerStatMusicFolderSettingName
            });

            var statVideoFolder = _processor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerStatVideoFolderSettingName
            });

            var statPictureFolder = _processor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerStatPictureFolderSettingName
            });

            // TODO : If folder is null then notify user

            var music = content.AsParallel().SingleOrDefault(x => statMusicFolder.Contains(x));
            var video = content.AsParallel()
                .SingleOrDefault(x => statVideoFolder.Contains(x)); // TODO : Fix if no Video exists
            var picture = content.AsParallel().SingleOrDefault(x => statPictureFolder.Contains(x));

            if (video == null)
            {
                var pictureFolder = _processor.Process(new GetFolderFromStringSettingQuery()
                {
                    SettingName = Constants.PlayerPictureFolderSettingName
                });
                video = content.AsParallel().SingleOrDefault(x => pictureFolder.Contains(x));
            }

            var res = new List<FileNode>()
            {
                music, video, picture
            }.Where(x => x != null);

            videoPlayer.Queue.SetQueue(res);

            Console.WriteLine("Using PlayerStatPlayStrategy");

            videoPlayer.PlayQueue();
        }
    }
}