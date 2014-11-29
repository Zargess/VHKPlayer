using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.FileManagement.Factories.PlayList;
using System;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayListContainer : IContainer {
        public ObservableCollection<IPlayable> Content { get; private set; }
        public string Name { get; private set; }
        private ILoadingStrategy<IPlayable> LoadingStrategy { get; set; }

        public PlayListContainer(ILoadingStrategy<IPlayable> loadingStrategy) {
            Content = new ObservableCollection<IPlayable>();
            Name = "PlayLister";
            LoadingStrategy = loadingStrategy;
            Load();
        }

        public void Load() {
            Content.Clear();
            LoadingStrategy.Load(Content);
        }
    }
}