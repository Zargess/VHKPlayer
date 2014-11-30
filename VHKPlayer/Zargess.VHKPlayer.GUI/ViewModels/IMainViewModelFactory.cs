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
