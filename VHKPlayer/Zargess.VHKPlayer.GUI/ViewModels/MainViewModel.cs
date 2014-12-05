﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.GUI.UtilClasses;

namespace Zargess.VHKPlayer.GUI.ViewModels {
    public class MainViewModel {
        public IFolder RootFolder { get; private set; }
        public ICompositeContainer MusicContainer { get; private set; }
        public IContainer PlayerContainer { get; private set; }
        public IContainer PlayListContainer { get; private set; }
        public IContainer CardContainer { get; private set; }
        public IContainer MiscContainer { get; private set; }
        public RelayCommand PlayablePressed { get; private set; }
        public RelayCommand PlayerPicPressed { get; private set; }
        public RelayCommand PlayerVidPressed { get; private set; }
        public RelayCommand PlayerStatPressed { get; private set; }

        public MainViewModel(IMainViewModelFactory factory) {
            RootFolder = factory.CreateFolder();
            MusicContainer = factory.CreateMusicContainer();
            PlayerContainer = factory.CreatePlayerContainer();
            PlayListContainer = factory.CreatePlayListContainer();
            CardContainer = factory.CreateCardContainer();
            MiscContainer = factory.CreateMiscContainer();
            PlayablePressed = new RelayCommand(PlayableClick);
            PlayerPicPressed = new RelayCommand(PlayerPicClick);
            PlayerVidPressed = new RelayCommand(PlayerVidClick);
            PlayerStatPressed = new RelayCommand(PlayerStatClick);
        }

        private void PlayableClick(object parameter) {
            var p = (FindParameters)parameter;
            Console.WriteLine(p.Playable.Name);
            Console.WriteLine(p.ControlName);
        }

        private void PlayerPicClick(object parameter) {

        }

        private void PlayerVidClick(object parameter) {

        }

        private void PlayerStatClick(object parameter) {

        }
    }
}