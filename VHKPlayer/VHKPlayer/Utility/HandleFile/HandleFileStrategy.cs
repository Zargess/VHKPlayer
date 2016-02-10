using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Queries.IsStatFile;
using VHKPlayer.Utility.FileDelayStrategy.Interfaces;
using VHKPlayer.Utility.HandleFile.Interfaces;
using VHKPlayer.Utility.PlayStrategy;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Utility.HandleFile
{
    public class HandleFileStrategy : IHandleFileStrategy
    {
        public void Handle(FileNode file, IVideoPlayerController controller, IPlayStrategy playStrategy, IQueryProcessor processor, IFileDelayStrategy delay)
        {
            var isStatFile = processor.Process(new IsStatFileQuery()
            {
                File = file
            });

            var isStatStrategy = playStrategy is PlayerStatPlayStrategy;

            if (isStatFile)
            {
                controller.ShowStats();
                delay.StartTimer();
            }
            else if (file.Type == FileType.Picture && isStatStrategy)
            {
                delay.StartTimer();
            }
        }
    }
}
