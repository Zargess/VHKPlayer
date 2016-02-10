using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using VHKPlayer.Controllers.Interfaces;
using VHKPlayer.Controls;
using VHKPlayer.Models;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Controllers
{
    public class PlayController : IPlayController, IStatObserver
    {
        private readonly MediaViewControl _mediaView;
        private Player _watchedPlayer;

        public PlayController(MediaViewControl mediaView)
        {
            _mediaView = mediaView;
        }

        public void Mute(FileType type)
        {
            GetMediaElement(type).IsMuted = !GetMediaElement(type).IsMuted;
        }

        public void Pause(FileType type)
        {
            GetMediaElement(type).Pause();
        }

        public void Play(FileNode file)
        {
            if (file.Type == FileType.Picture)
            {
                ShowPicture(file);
            }
            else
            {
                if (file.Type == FileType.Video) _mediaView.Picture.Visibility = Visibility.Collapsed;

                var mediaelement = GetMediaElement(file.Type);
                mediaelement.Source = new Uri(file.FullPath);

                if (file.Type == FileType.Video) _mediaView.Video.Visibility = Visibility.Visible;

                mediaelement.Play();
                Console.WriteLine("PlayController playing: {0}", file.Name);
            }
        }

        public void Resume(FileType type)
        {
            GetMediaElement(type).Play();
        }

        public void ShowStats(Player p)
        {
            if (!_mediaView.StatsEnabled) return;
            _watchedPlayer = p;

            _watchedPlayer.AddObserver(this);

            UpdateLabels(_watchedPlayer.Stats);

            Console.WriteLine("Showing stats for: {0}", p.Name);
        }

        public void Stop(FileType type)
        {
            GetMediaElement(type).Stop();
        }

        public void Notify(Statistics stats)
        {
            UpdateLabels(stats);
        }

        private void ShowPicture(FileNode file)
        {
            _mediaView.Video.Visibility = Visibility.Collapsed;

            _mediaView.Picture.Source = ConstructImage(file);

            _mediaView.Picture.Visibility = Visibility.Visible;
        }

        private MediaElement GetMediaElement(FileType type)
        {
            if (type == FileType.Audio) return _mediaView.Audio;
            return _mediaView.Video;
        }

        private BitmapImage ConstructImage(FileNode file)
        {
            var res = new BitmapImage();
            res.BeginInit();
            res.UriSource = new Uri(file.FullPath);
            res.EndInit();
            return res;
        }

        private void UpdateLabels(Statistics stats)
        {
            throw new NotImplementedException();
        }
    }
}
