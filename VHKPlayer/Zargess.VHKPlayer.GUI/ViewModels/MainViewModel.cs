using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.GUI.ViewModels {
    public class MainViewModel {
        public IFolder RootFolder { get; private set; }
        public ICompositeContainer MusicContainer { get; private set; }
        public IContainer PlayerContainer { get; private set; }
        public IContainer PlayListContainer { get; private set; }
        public IContainer CardContainer { get; private set; }
        public IContainer MiscContainer { get; private set; }

        public MainViewModel(IMainViewModelFactory factory) {
            RootFolder = factory.CreateFolder();
            MusicContainer = factory.CreateMusicContainer();
            PlayerContainer = factory.CreatePlayerContainer();
            PlayListContainer = factory.CreatePlayListContainer();
            CardContainer = factory.CreateCardContainer();
            MiscContainer = factory.CreateMiscContainer();
        }
    }
}