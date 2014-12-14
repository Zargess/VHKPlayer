using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Zargess.VHKPlayer.Commands;
using Zargess.VHKPlayer.EventHandlers;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.ViewModels {
    public class VideoPlayerViewModel : IPlayController {
        public IContainer<IContainer<IPlayable>> MusicContainer { get; private set; }
        public IContainer<IPlayable> PlayerContainer { get; private set; }
        public IContainer<IPlayable> PlayListContainer { get; private set; }
        public IContainer<IPlayable> CardContainer { get; private set; }
        public IContainer<IPlayable> MiscContainer { get; private set; }
        public List<IFolder> PlayerFolders { get; private set; }
        public RelayCommand PlayablePressed { get; private set; }
        public RelayCommand Test { get; private set; }
        public INotificationContainer NotifiContainer { get; private set; }

        public event PlayerFunctionHandler PlayerFunction;


        public VideoPlayerViewModel(IVideoPlayerFactory factory) {
            // TODO : Implement a refresh function
            MusicContainer = factory.CreateMusicContainer();
            PlayerContainer = factory.CreatePlayerContainer();
            PlayListContainer = factory.CreatePlayListContainer();
            CardContainer = factory.CreateCardContainer();
            MiscContainer = factory.CreateMiscContainer();
            // TODO : Move this to the factory
            PlayablePressed = new RelayCommand(PlayableClick);
            Test = new RelayCommand(TestClick);
            NotifiContainer = new NotificationContainer();
            NotifiContainer.Add(new Notification("Test"));
        }

        private void PlayableClick(object parameter) {
            var item = (FindParameters)parameter;
            Console.WriteLine(item.ControlName);
            Console.WriteLine(item.Playable);
        }

        private void TestClick(object parameter) {
            NotifiContainer.Add(new Notification("Lolz"));
        }

        public void PlayVideo() {
            throw new NotImplementedException();
        }

        public void PlayMusic() {
            throw new NotImplementedException();
        }
    }
}
