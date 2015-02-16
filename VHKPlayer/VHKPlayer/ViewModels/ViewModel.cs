using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;

namespace VHKPlayer.ViewModels {
    public class ViewModel : IViewModel {
        // TODO : Implement the viewmodel
        // TODO : Make a Dictionary of commands linking to the IVideoPlayer interface
        public bool AutoPlayListEnabled {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public IVideoPlayer VideoPlayer {
            get {
                throw new NotImplementedException();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
