using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Strategies.Playing {
    public class PlayPlayerStatStrategy : IPlayStrategy {
        private int _counter;
        private IVideoPlayer _videoplayer;

        public PlayPlayerStatStrategy() {
            Settings.PlayerStatTimer = new DispatcherTimer();
            Settings.PlayerStatTimer.Interval = new TimeSpan(0,0,1);
            Settings.PlayerStatTimer.Tick += PlayerStatTimer_Tick;
        }

        private void PlayerStatTimer_Tick(object sender, EventArgs e) {
            _counter++;
            var statstime = (int)Settings.FolderConfig.Get("timeStatShown");
            var done = statstime < _counter;
            if (!done) return;
            Settings.CancelStatTimer();
            _videoplayer.PlayQueue();
        }

        private void StartTimer() {
            _counter = 0;
            Settings.PlayerStatTimer.Start();
        }

        public void Play(IVideoPlayer videoplayer, Queue<IFile> queue, IPlayable playable, PlayType type) {
            var file = queue.Dequeue();
            _videoplayer = videoplayer;
            _videoplayer.Play(file);
            if (file.Type != FileType.Picture) return;
            StartTimer();
            if (!Settings.PlayerStatPictureFolder.ContainsFile(file)) return;
            _videoplayer.ShowStats();
        }
    }
}
