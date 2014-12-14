using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Factories.IPlayers;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Model {
    public class PlayListContainer : IContainer<IPlayable> {
        public ObservableCollection<IPlayable> Content { get; private set; }
        public string Name { get; private set; }
        private ILoadingStrategy<IPlayable> LoadingStrategy { get; set; }

        public PlayListContainer(ILoadingStrategy<IPlayable> loadingStrategy) {
            Content = new SortableCollection<IPlayable>();
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
