using System;
using System.Collections.Generic;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Queries.IsStatFile;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Controllers
{
    public class VideoPlayer : IVideoPlayer
    {
        private List<IPlayController> observers;
        private IPlayStrategy videoPlayStrategy;
        private IPlayable previousMusicPlayable, previousVideoPlayable;
        private IQueryProcessor processor;

        public bool AutoPlayList { get; set; }
        public Queue<FileNode> Queue { get; private set; }


        public VideoPlayer(IQueryProcessor processor)
        {
            this.processor = processor;
            observers = new List<IPlayController>();
            Queue = new Queue<FileNode>();
        }

        public void AddObserver(IPlayController observer)
        {
            observers.Add(observer);
        }

        public FileNode HintNext()
        {
            return videoPlayStrategy.PeekNext(this);
        }

        public void Mute(FileType type)
        {
            observers.ForEach(x => x.Mute(type));
        }

        public void Pause(FileType type)
        {
            observers.ForEach(x => x.Pause(type));
        }

        public void Play(FileNode file)
        {
            var isStatFile = processor.Process(new IsStatFileQuery()
            {
                File = file
            });

            if (isStatFile) ShowStats();

            observers.ForEach(x => x.Play(file));
        }

        public void Play(IPlayable playable, IPlayStrategy strategy)
        {
            if (playable is PlayableFile)
            {
                var playableFile = playable as PlayableFile;
                if (playableFile.File.Type == FileType.Audio)
                {
                    previousMusicPlayable = playableFile;
                } else
                {
                    previousVideoPlayable = playableFile;
                    videoPlayStrategy = strategy;
                }
            } else
            {
                previousVideoPlayable = playable;
                videoPlayStrategy = strategy;
            }

            playable.Play(strategy, this);
        }

        public void PlayQueue()
        {
            if (!Queue.IsEmpty())
            {
                var file = Queue.Dequeue();
                Play(file);
            } else if (videoPlayStrategy.Repeat)
            {
                previousVideoPlayable.Play(videoPlayStrategy, this);
            } else if (AutoPlayList)
            {
                // TODO : Make an option if auto advertisements is enabled
            }
        }

        public void Resume(FileType type)
        {
            observers.ForEach(x => x.Resume(type));
        }

        public void ShowStats()
        {
            if (!(previousVideoPlayable is Player)) return;
            var player = previousVideoPlayable as Player;
            observers.ForEach(x => x.ShowStats(player));
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
