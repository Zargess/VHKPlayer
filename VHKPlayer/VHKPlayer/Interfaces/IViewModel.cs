using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interfaces {
    public interface IViewModel : INotifyPropertyChanged {
        IVideoPlayer VideoPlayer { get; }
        bool AutoPlayListEnabled { get; set; }
    }
}
