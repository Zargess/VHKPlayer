using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Observers {
    public class PlayObserver : IPlayObserver, IPlayerObserver {
        private MediaElement _viewer;
        private MediaElement _audio;
        private Image _viewport;
        private IFile _currentMusic;
        private IFile _currentVideo;
        private IFile _currentPicture;
        private IPlayer _currentStatPlayer;
        private bool _allowAudio;
        private bool _allowStat;
        public ISoundStrategy SoundStrategy { get; private set; }

        public PlayObserver(MediaElement viewer, MediaElement audio, Image viewport, bool allowAudio, bool allowStat, ISoundStrategy soundStrategy) {
            _viewer = viewer;
            _audio = audio;
            this._viewport = viewport;
            _viewer.MediaEnded += Viewer_MediaEnded;
            _allowAudio = allowAudio;
            _allowStat = allowStat;
            SoundStrategy = soundStrategy;
        }

        private void Viewer_MediaEnded(object sender, RoutedEventArgs e) {
            Console.WriteLine("Ended");
            App.PlayManager.PlayQueue();
        }

        public void Mute(FileType type) {
            if (!_allowAudio) return;
            var player = GetMediaElement(type);
            if (player.Source == null) return;
            var muted = player.IsMuted;
            
            if(muted) {
                player.IsMuted = false;
                SoundStrategy.Starting();
            } else {
                SoundStrategy.Stoping(() => { player.IsMuted = true; });
            }
        }

        public void Pause(FileType type) {
            var player = GetMediaElement(type);
            if (player.Source == null) return;
            SoundStrategy.Stoping(player.Pause);
            player.Pause();
        }
		
        public void Stop(FileType type) {
            var player = GetMediaElement(type);
            if (player.Source == null) return;
            SoundStrategy.Stoping(player.Stop);
        }

        public void Resume(FileType type) {
            var player = GetMediaElement(type);
            if (player.Source == null) return;
            player.Play();
            SoundStrategy.Starting();
            if (type != FileType.Video) return;
            App.PlayerViewModel.ViewerVisible = Visibility.Visible;
        }

        public void Play(FileType type) {
            if (_currentStatPlayer != null) _currentStatPlayer.RemoveObserver(this);
            if (type == FileType.Picture) ShowPicture();
            else PlayFile(type);
        }

        private void PlayFile(FileType type) {
            if (type == FileType.Video) App.PlayerViewModel.HideStats();
            else if (type == FileType.Music) Mute(FileType.Video);

            var player = GetMediaElement(type);
            var file = GetFile(type);

            player.Source = new Uri(file.FullPath);
            player.Play();

            SoundStrategy.Starting();
            Console.WriteLine("Playing {0}", file.Name);
        }

        private void ShowPicture() {
            _viewport.Source = GeneralFunctions.ConstructImage(_currentPicture);
            if (_currentPicture.Source == "PlayerVideoStat") App.PlayerViewModel.ShowStats();
            else App.PlayerViewModel.ShowPicture();
        }

        public void SetCurrentFile(IFile file) {
            if (file.Type == FileType.Video) {
                _currentVideo = file;
            } else if (file.Type == FileType.Music) {
                _currentMusic = file;
            } else if (file.Type == FileType.Picture) {
                _currentPicture = file;
            }
        }

        private MediaElement GetMediaElement(FileType type) {
            if (type == FileType.Music) return _audio;
            return _viewer;
        }

        private IFile GetFile(FileType type) {
            if (type == FileType.Video) {
                return _currentVideo;
            } else if (type == FileType.Music) {
                return _currentMusic;
            } else if (type == FileType.Picture) {
                return _currentPicture;
            } else return null;
        }

        public void ShowStats(IPlayer player) {
            if (!_allowStat) return;
            App.PlayerViewModel.ShowStats();
            _currentStatPlayer = player;
            player.AddObserver(this);
            StatsChanged(player.Stats);
        }

        public void StatsChanged(Statistics stat) {
            var scorings = stat.Goals + "/" + stat.Shots;
            var penalties = stat.Suspension + "";
            App.PlayerViewModel.Scorings = scorings;
            App.PlayerViewModel.Penalties = penalties;
        }
    }
}