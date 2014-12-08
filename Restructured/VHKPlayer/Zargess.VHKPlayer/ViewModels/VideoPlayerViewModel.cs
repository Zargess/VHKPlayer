using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Zargess.VHKPlayer.Commands;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.ViewModels {
    public class VideoPlayerViewModel {
        public IContainer<IContainer<IPlayable>> MusicContainer { get; private set; }
        public IContainer<IPlayable> PlayerContainer { get; private set; }
        public IContainer<IPlayable> PlayListContainer { get; private set; }
        public IContainer<IPlayable> CardContainer { get; private set; }
        public IContainer<IPlayable> MiscContainer { get; private set; }
        public List<IFolder> PlayerFolders { get; private set; }
        public RelayCommand PlayablePressed { get; private set; }

        public VideoPlayerViewModel(IVideoPlayerFactory factory) {
            // TODO : Implement a refresh function
            MusicContainer = factory.CreateMusicContainer();
            PlayerContainer = factory.CreatePlayerContainer();
            PlayListContainer = factory.CreatePlayListContainer();
            CardContainer = factory.CreateCardContainer();
            MiscContainer = factory.CreateMiscContainer();
            PlayablePressed = new RelayCommand(PlayableClick);
        }

        private void PlayableClick(object parameter) {
            var item = (FindParameters)parameter;
            Console.WriteLine(item.ControlName);
            Console.WriteLine(item.Playable);
        }
    }
}
