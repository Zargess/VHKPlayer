﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zargess.VHKPlayer.Commands;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.EventHandlers;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.PlayManaging;
using Zargess.VHKPlayer.Strategies.Playing;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.ViewModels {
    public class VideoPlayerViewModel : INotifyPropertyChanged {
        private IContainer<IContainer<IPlayable>> _musicContainer;
        public IContainer<IContainer<IPlayable>> MusicContainer {
            get {
                return _musicContainer;
            }
            set {
                _musicContainer = value;
                RaisePropertyChanged("MusicContainer");
            }
        }

        private IContainer<IPlayable> _playerContainer;
        public IContainer<IPlayable> PlayerContainer {
            get {
                return _playerContainer;
            }
            set {
                _playerContainer = value;
                RaisePropertyChanged("PlayerContainer");
            }
        }

        private IContainer<IPlayable> _playListContainer;
        public IContainer<IPlayable> PlayListContainer {
            get {
                return _playListContainer;
            }
            set {
                _playListContainer = value;
                RaisePropertyChanged("PlayListContainer");
            }
        }

        private IContainer<IPlayable> _cardContainer;
        public IContainer<IPlayable> CardContainer {
            get {
                return _cardContainer;
            }
            set {
                _cardContainer = value;
                RaisePropertyChanged("CardContainer");
            }
        }

        private IContainer<IPlayable> _miscContainer;
        public IContainer<IPlayable> MiscContainer {
            get {
                return _miscContainer;
            }
            set {
                _miscContainer = value;
                RaisePropertyChanged("MiscContainer");
            }
        }

        // TODO : Consider moving this later on
        private Visibility _viewerVisible;
        public Visibility ViewerVisible {
            get {
                return _viewerVisible;
            }
            set {
                _viewerVisible = value;
                RaisePropertyChanged("ViewerVisible");
            }
        }

        public RelayCommand PlayablePressed { get; private set; }
        public RelayCommand Test { get; private set; }
        public INotificationContainer NotifiContainer { get; private set; }
        public Action<VideoPlayerViewModel> ReloadAction { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public VideoPlayerViewModel(IVideoPlayerFactory factory) {
            ViewerVisible = Visibility.Visible;
            // TODO : test the refresh function
            MusicContainer = factory.CreateMusicContainer();
            PlayerContainer = factory.CreatePlayerContainer();
            PlayListContainer = factory.CreatePlayListContainer();
            CardContainer = factory.CreateCardContainer();
            MiscContainer = factory.CreateMiscContainer();
            ReloadAction = factory.CreateReloadPolicy();
            App.ConfigService.PropertyChanged += (sender, ee) => ReloadAction(this);
            // TODO : Move this to the factory
            PlayablePressed = new RelayCommand(PlayableClick);
            Test = new RelayCommand(TestClick);
            NotifiContainer = new NotificationContainer();
            NotifiContainer.Add(new Notification("Test"));
        }

        private void RaisePropertyChanged(string name) {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void PlayableClick(object parameter) {
            var item = (FindParameters)parameter;
            Console.WriteLine(item.ControlName);
            Console.WriteLine(item.Playable);
            App.PlayManager.Play(item.Playable, GeneralFunctions.GetPlayType(item.ControlName));
        }

        private void TestClick(object parameter) {
            NotifiContainer.Add(new Notification("Lolz"));
        }
    }
}