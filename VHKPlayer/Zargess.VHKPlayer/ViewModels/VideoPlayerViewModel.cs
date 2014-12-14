using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Zargess.VHKPlayer.Commands;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.ViewModels {
    public class VideoPlayerViewModel : INotifyPropertyChanged {
        public IContainer<IContainer<IPlayable>> MusicContainer { get; private set; }
        public IContainer<IPlayable> PlayerContainer { get; private set; }
        public IContainer<IPlayable> PlayListContainer { get; private set; }
        public IContainer<IPlayable> CardContainer { get; private set; }

        public IContainer<IPlayable> MiscContainer { get; private set; }

        public List<IFolder> PlayerFolders { get; private set; }
        public RelayCommand PlayablePressed { get; private set; }
        public RelayCommand Test { get; private set; }
        public INotificationContainer NotifiContainer { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private IFile _currentVideoFile;
        public IFile CurrentVideoFile {
            get {
                return _currentVideoFile;
            }
            set {
                _currentVideoFile = value;
                PropertyChange("CurrentVideoFile");
            }
        }

        private IFile _currentMusicFile;
        public IFile CurrentMusicFile {
            get {
                return _currentMusicFile;
            }
            set {
                _currentMusicFile = value;
                PropertyChange("CurrentMusicFile");
            }
        }

        private IFile _currentPictureFile;
        public IFile CurrentPictureFile {
            get {
                return _currentPictureFile;
            }
            set {
                _currentPictureFile = value;
                PropertyChange("CurrentPictureFile");
            }
        }


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

        private void PropertyChange(string name) {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
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
