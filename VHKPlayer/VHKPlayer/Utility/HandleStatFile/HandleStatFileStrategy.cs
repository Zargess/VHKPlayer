using System;
using System.Timers;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetIntSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.HandleStatFile.Interfaces;

namespace VHKPlayer.Utility.HandleStatFile
{
    public class HandleStatFileStrategy : IHandleStatFileStrategy
    {
        private IVideoPlayerController _controller;
        private readonly IQueryProcessor _processor;
        private readonly Timer _timer;

        public HandleStatFileStrategy(IQueryProcessor processor)
        {
            _processor = processor;
            _timer = new Timer();
            _timer.Elapsed += _timer_Elapsed;
        }

        public void HandleFile(IVideoPlayerController controller, FileNode file)
        {
            _controller = controller;

            var delay = _processor.Process(new GetIntSettingQuery
            {
                SettingName = Constants.StatTimerSettingName
            });
            // TODO : Fix timer? - Might work.
            _timer.Interval = delay;
            _timer.Enabled = true;
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Enabled = false;
            _timer.Stop();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _controller.PlayQueue();
            _timer.Enabled = false;
            _timer.Stop();
            Console.WriteLine("Stopping timer!");
        }
    }
}