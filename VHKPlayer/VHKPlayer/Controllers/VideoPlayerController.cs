using System;
using System.Collections.Generic;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Queries.IsStatFile;
using VHKPlayer.Utility.HandleStatFile.Interfaces;
using VHKPlayer.Utility.PlayQueueStrategy.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Controllers
{
    public class VideoPlayerController : IVideoPlayerController
    {
        private readonly List<IPlayController> _observers;
        private IPlayStrategy _videoPlayStrategy;
        private IPlayable _previousMusicPlayable, _previousVideoPlayable;
        private readonly IQueryProcessor _processor;
        private readonly IPlayQueueStrategy _playQueue;
        private readonly IHandleStatFileStrategy _handleStatFile;

        public bool AutoPlayList { get; set; }
        public Queue<FileNode> Queue { get; private set; }


        public VideoPlayerController(IQueryProcessor processor, IPlayQueueStrategy playQueue, IHandleStatFileStrategy handleStatFile)
        {
            this._processor = processor;
            _observers = new List<IPlayController>();
            Queue = new Queue<FileNode>();
            this._playQueue = playQueue;
            _handleStatFile = handleStatFile;
        }

        public void AddObserver(IPlayController observer)
        {
            _observers.Add(observer);
        }

        public FileNode HintNext()
        {
            return _videoPlayStrategy.PeekNext(this);
        }

        public void Mute(FileType type)
        {
            _observers.ForEach(x => x.Mute(type));
        }

        public void Pause(FileType type)
        {
            _observers.ForEach(x => x.Pause(type));
        }

        public void Play(FileNode file)
        {
            var isStatFile = _processor.Process(new IsStatFileQuery()
            {
                File = file
            });

            if (isStatFile)
            {
                ShowStats();
                _handleStatFile.HandleFile(this, file);
            }
            Console.WriteLine(file.Name);
            _observers.ForEach(x => x.Play(file));
        }

        public void Play(IPlayable playable, IPlayStrategy strategy)
        {
            _handleStatFile.StopTimer();

            if (playable is PlayableFile)
            {
                var playableFile = playable as PlayableFile;
                if (playableFile.File.Type == FileType.Audio)
                {
                    _previousMusicPlayable = playableFile;
                } else
                {
                    _previousVideoPlayable = playableFile;
                    _videoPlayStrategy = strategy;
                }
            } else
            {
                _previousVideoPlayable = playable;
                _videoPlayStrategy = strategy;
            }

            playable.Play(strategy, this);

            // TODO : Remove this print
            Console.WriteLine("Video Player Controller: {0}", playable.Name);
        }

        public void PlayQueue()
        {
            _playQueue.PlayNextItem(Queue, this, _videoPlayStrategy, _previousVideoPlayable);
        }

        public void Resume(FileType type)
        {
            _observers.ForEach(x => x.Resume(type));
        }

        public void ShowStats()
        {
            if (!(_previousVideoPlayable is Player)) return;
            var player = _previousVideoPlayable as Player;
            _observers.ForEach(x => x.ShowStats(player));
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public void Stop(FileType type)
        {
            _observers.ForEach(x => x.Stop(type));
        }
    }
}
