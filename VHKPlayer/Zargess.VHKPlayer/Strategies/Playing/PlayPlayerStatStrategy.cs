using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Playing {
    public class PlayPlayerStatStrategy : IPlayFileStrategy {
        private static Timer PictureTimer { get; set; }
        private static int Counter { get; set; }

        public PlayPlayerStatStrategy() {
            PictureTimer = new Timer();
            PictureTimer.Interval = 1000.0;
            PictureTimer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e) {
            Counter++;
            var showStat = (int)App.GuiConfigService.Get("timeStatShown");
            var done = showStat <= Counter;
            if (!done) return;
            Action act = new Action(() => App.PlayManager.PlayQueue());
            CancelTimer();
            Application.Current.Dispatcher.BeginInvoke(act);
        }

        public void Play(IFile file, PlayType type) {
            App.PlayManager.SetCurrentFile(file);
            App.PlayManager.Play(file.Type);
            if (file.Type != FileType.Picture) return;
            StartTimer();
            // TODO : Find a way to check in settings if this is the stat picture folder
            if (!App.StatPictureFolder.ContainsFile(file)) return;
            App.PlayManager.ShowStats();
        }

        private void StartTimer() {
            PictureTimer.Enabled = true;
            PictureTimer.Start();
        }

        public static void CancelTimer() {
            PictureTimer.Enabled = false;
            PictureTimer.Stop();
            Counter = 0;
        }
    }
}