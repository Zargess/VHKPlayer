using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.ViewModels;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IMediaViewModel {
        VideoPlayerViewModel ViewModel { get; }
        IPlayObserver Observer { get; }
    }
}
