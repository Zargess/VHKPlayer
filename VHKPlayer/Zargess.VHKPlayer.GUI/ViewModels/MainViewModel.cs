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
        public ObservableCollection<IContainer> MusicCollections { get; private set; }
        public IContainer PlayerContainer { get; private set; }
        public IContainer PlayListContainer { get; private set; }
        public IContainer CardContainer { get; private set; }
        public IContainer BeforeAndAfter { get; private set; }

        public MainViewModel(IMainViewModelFactory factory) {
            Load(factory);
        }

        private void Load(IMainViewModelFactory factory) {
            throw new NotImplementedException();
        }
    }
}
