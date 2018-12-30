using System;
using System.Timers;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Queries.GetIntSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.FileDelayStrategy.Interfaces;

namespace VHKPlayer.Utility.FileDelayStrategy
{
    public class FileDelayStrategy : IFileDelayStrategy
    {
        private IVideoPlayerController _controller;
        private readonly IQueryProcessor _processor;
        private readonly Timer _timer;

        public FileDelayStrategy(IQueryProcessor processor, IVideoPlayerController controller)
        {
            _processor = processor;
            _controller = controller;
            _timer = new Timer();
            _timer.Elapsed += _timer_Elapsed;
        }

        public void StartTimer()
        {
            var delay = _processor.Process(new GetIntSettingQuery
            {
                SettingName = Constants.StatTimerSettingName
            });

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
            App.Dispatch.BeginInvoke(new Action(() => _controller.PlayQueue()));
            _timer.Enabled = false;
            _timer.Stop();
            Console.WriteLine("Stopping timer!");
        }
    }
}