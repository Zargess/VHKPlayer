using System.Collections.Generic;
using VHKPlayer.Controllers;
using VHKPlayer.Infrastructure;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Queries.GetAutoPlayList;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.PlayQueueStrategy.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Utility.PlayQueueStrategy
{
    public class MusicVideoPlayQueueStrategy : IPlayQueueStrategy
    {
        private IQueryProcessor _processor;

        public MusicVideoPlayQueueStrategy(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public void PlayNextItem(Queue<FileNode> queue, VideoPlayerController controller, IPlayStrategy strategy, IPlayable previous)
        {
            if (!controller.Queue.IsEmpty())
            {
                var file = queue.Dequeue();
                controller.Play(file);
                if (file.Type == FileType.Audio && queue.Peek().Type != FileType.Audio)
                {
                    controller.Play(queue.Dequeue());
                }
            }
            else if (strategy.Repeat)
            {
                previous.Play(strategy, controller);
            }
            else if (controller.AutoPlayList)
            {
                var playlist = _processor.Process(new GetAutoPlayListQuery());
                if (playlist == null) return;
                playlist.Play(null, controller);
            }
        }
    }
}
