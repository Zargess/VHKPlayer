using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.GUI.ViewModels {
    public interface IMainViewModelFactory {
        IFolder CreateFolder();
        ICompositeContainer CreateMusicContainer();
        IContainer CreatePlayerContainer();
        IContainer CreatePlayListContainer();
        IContainer CreateCardContainer();
        IContainer CreateMiscContainer();
        IContainer CreatePlayerOut();
    }
}
