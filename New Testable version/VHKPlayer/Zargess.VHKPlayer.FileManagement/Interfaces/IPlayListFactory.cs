using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Factories {
    public interface IPlayListFactory {
        string CreateName();
        IFolder CreateFolder();
        IFileSelectionStrategy CreateSelectionStrategy();
        ILoadingStrategy CreateLoadingStrategy();
    }
}