using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Observers {
    public class PlayObserver : IPlayObserver {
        private MediaElement Viewer { get; set; }
        private MediaElement Audio { get; set; }
        private IFile CurrentMusicFile { get; set; }
        private IFile CurrentPictureFile { get; set; }
        private IFile CurrentVideoFile { get; set; }
        private bool AllowAudio { get; set; }
        private bool AllowStat { get; set; }

        public PlayObserver(MediaElement viewer, MediaElement audio, bool allowAudio, bool allowStat) {
            Viewer = viewer;
            Audio = audio;
            Viewer.MediaEnded += Viewer_MediaEnded;
            AllowAudio = allowAudio;
            AllowStat = allowStat;
        }

        private void Viewer_MediaEnded(object sender, RoutedEventArgs e) {
            Console.WriteLine("Ended");
            App.PlayManager.PlayQueue();
        }

        public void Mute(FileType type) {
            if (!AllowAudio) return;
            var player = GetMediaElement(type);
            if (player.Source == null) return;
            player.IsMuted = !player.IsMuted;
        }

        public void Pause(FileType type) {
            var player = GetMediaElement(type);
            if (player.Source == null) return;
            player.Pause();
        }

        // TODO : At the start of this method unsubscribe from current players stats
        public void Play(FileType type) {
            var player = GetMediaElement(type);
            var file = GetFile(type);
            player.Source = new Uri(file.FullPath);
            player.Play();
            Console.WriteLine("Playing {0}", file.Name);
        }

        public void Stop(FileType type) {
            var player = GetMediaElement(type);
            if (player.Source == null) return;
            player.Stop();
        }

        public void Resume(FileType type) {
            var player = GetMediaElement(type);
            if (player.Source == null) return;
            player.Play();
            if (type != FileType.Video) return;
            // TODO : Make video mediaelement visable
        }

        public void SetCurrentFile(IFile file) {
            if (file.Type == FileType.Video) {
                CurrentVideoFile = file;
            } else if (file.Type == FileType.Music) {
                CurrentMusicFile = file;
            } else if (file.Type == FileType.Picture) {
                CurrentPictureFile = file;
            }
        }

        private MediaElement GetMediaElement(FileType type) {
            if (type == FileType.Music) return Audio;
            return Viewer;
        }

        private IFile GetFile(FileType type) {
            if (type == FileType.Video) {
                return CurrentVideoFile;
            } else if (type == FileType.Music) {
                return CurrentMusicFile;
            } else if (type == FileType.Picture) {
                return CurrentPictureFile;
            } else return null;
        }

        // TODO : Consider making an event method to bind to
        public void ShowStats(IPlayable currentPlayable) {
            if (!AllowStat) return;
            App.PlayerViewModel.ViewPortVisibility = Visibility.Visible;
            App.PlayerViewModel.ViewerVisible = Visibility.Collapsed;
        }
    }
}