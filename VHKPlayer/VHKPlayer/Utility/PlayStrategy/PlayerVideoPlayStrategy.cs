﻿using Autofac;
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
    public class PlayerVideoPlayStrategy : IPlayStrategy
    {
        private bool _repeat;
        private readonly IQueryProcessor _processor;

        public bool Repeat
        {
            get
            {
                return _repeat;
            }

            set
            {
                _repeat = value;
            }
        }

        public PlayerVideoPlayStrategy()
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
            var folder = _processor.Process(new GetFolderFromStringSettingQuery()
            {
                SettingName = Constants.PlayerVideoFolderSettingName
            });

            var video = content.AsParallel().SingleOrDefault(x => folder.Contains(x));

            if (video == null) return;

            videoPlayer.Queue.SetQueue(new List<FileNode>()
            {
                video
            });

            videoPlayer.PlayQueue();
        }
    }
}