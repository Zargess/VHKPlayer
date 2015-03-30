using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Controllers {
    public class PlayController : IPlayController, IPlayerObserver {
        private MediaElement _viewer, _audio;
        private Image _viewport;
        private bool _allowAduio, _allowStat;
        private ISoundStrategy _soundStrategy;

        public PlayController(MediaElement viewer, MediaElement audio, Image viewport, bool allowAudio, bool allowStat, ISoundStrategy soundStrategy) {

        }

        public void Mute(FileType type) {
            throw new NotImplementedException();
        }

        public void Pause(FileType type) {
            throw new NotImplementedException();
        }

        public void Play(IFile file) {
            if (file.Type == FileType.Picture) {
                ShowImage(file);
                return;
            }
            var mediaElement = SelectMediaElement(file.Type);

        }

        public void Resume(FileType type) {
            throw new NotImplementedException();
        }

        public void ShowStats(IPlayer player) {
            throw new NotImplementedException();
        }

        public void Stop(FileType type) {
            throw new NotImplementedException();
        }

        public void Update(IFile file) {
            throw new NotImplementedException();
        }

        private MediaElement SelectMediaElement(FileType type) {
            if (type == FileType.Video) return _viewer;
            else return _audio;
        }

        private void ShowImage(IFile file) {
            if (!file.Exists()) return;
            _viewport.Source = GeneralFunctions.ConstructImage(file);
            _viewer.Visibility = Visibility.Hidden;
        }

        public void StatsChanged(IStatistics stat) {
            throw new NotImplementedException();
        }
    }
}
