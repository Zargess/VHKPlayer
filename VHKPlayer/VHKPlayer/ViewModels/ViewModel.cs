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
        private bool _autoPlayListEnabled;
        public bool AutoPlayListEnabled {
            get {
                return _autoPlayListEnabled;
            }

            set {
                _autoPlayListEnabled = value;
                RaisePropertyChanged("AutoPlayListEnabled");
            }
        }

        public IVideoPlayer VideoPlayer { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public ViewModel() {
            AutoPlayListEnabled = false;
        }

        private void RaisePropertyChanged(string name) {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
