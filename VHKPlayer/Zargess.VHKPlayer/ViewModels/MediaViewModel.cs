using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Observers;

namespace Zargess.VHKPlayer.ViewModels {
    public class MediaViewModel : IMediaViewModel { 
        public IPlayObserver Observer { get; private set; }

        public VideoPlayerViewModel ViewModel {
            get {
                return App.PlayerViewModel;
            }
        }

        public MediaViewModel(MediaElement video, MediaElement audio, Image viewport, bool allowAudio, bool allowStat) {
            Observer = new PlayObserver(video, audio, viewport, allowAudio, allowStat);
        }
    }
}