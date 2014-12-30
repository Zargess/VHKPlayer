using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Sound {
    public class GeneralSoundStrategy : ISoundStrategy {
        private ISoundStrategy _noFadeStrategy;
        private ISoundStrategy _fadeStrategy;
        private ISoundStrategy _currentStrategy;
        private MediaElement _video;
        private MediaElement _audio;
        private DispatcherTimer _timer;
        private bool _fadeOutActive;

        public GeneralSoundStrategy(MediaElement video, MediaElement audio, ISoundStrategy noFade, ISoundStrategy fade) {
            _video = video;
            _audio = audio;
            _noFadeStrategy = noFade;
            _fadeStrategy = fade;
            _currentStrategy = _noFadeStrategy;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += ticktock;

            _fadeOutActive = false;
            
            _timer.Start();
        }

        private void ticktock(object sender, EventArgs e) {
            if (_fadeOutActive) return;
            var videoPlaying = FilePlaying(_video);
            var audioPlaying = FilePlaying(_audio);
            if (videoPlaying || audioPlaying) return;
            _fadeOutActive = true;
            _fadeStrategy.Stoping(() => { _fadeOutActive = false; });
        }

        private bool FilePlaying(MediaElement me) {
            if (me.Source == null) return false;
            if (!me.NaturalDuration.HasTimeSpan) return true;
            var position = me.Position.Seconds;
            var duration = me.NaturalDuration.TimeSpan.Minutes * 60 + me.NaturalDuration.TimeSpan.Seconds;
            var fadetime = (int)App.GuiConfigService.Get("fadeDuration");
            var time = duration - position;
            return time > fadetime && HasAudio();
        }

        public void Starting() {
            SetCurrent();
            _currentStrategy.Starting();
        }

        public void Stoping(Action callback) {
            SetCurrent();
            _currentStrategy.Stoping(callback);
        }

        private void SetCurrent() {
            var fadeActive = (bool)App.GuiConfigService.Get("fadeSound");

            _currentStrategy = fadeActive && HasAudio() ? _fadeStrategy : _noFadeStrategy;
        }

        public void StopFadeManagerThread() {
            _timer.Stop();
        }

        private bool HasAudio() {
            var res = true;
            if (App.PlayManager.CurrentPlayable is IPlayList) {
                var playlist = (IPlayList)App.PlayManager.CurrentPlayable;
                res = playlist.HasAudio && !FilePlaying(_audio);
            }
            Console.WriteLine(res);
            return res;
        }
    }
}