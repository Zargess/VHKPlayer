using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.ViewModels {
    public class VideoPlayerViewModel {
        public IContainer<IContainer<IPlayable>> MusicContainer { get; private set; }
        public IContainer<IPlayable> PlayerContainer { get; private set; }
        public IContainer<IPlayable> PlayListContainer { get; private set; }
        public IContainer<IPlayable> CardContainer { get; private set; }
        public IContainer<IPlayable> MiscContainer { get; private set; }

        public VideoPlayerViewModel(IVideoPlayerFactory factory) {
            MusicContainer = factory.CreateMusicContainer();
            PlayerContainer = factory.CreatePlayerContainer();
            PlayListContainer = factory.CreatePlayListContainer();
            CardContainer = factory.CreateCardContainer();
            MiscContainer = factory.CreateMiscContainer();
        }
    }
}
