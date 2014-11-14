

namespace Zargess.VHKPlayer.FileManagement {
    public interface IPlayList : IPlayable, IWatchable {
        void Add(IFile p);
    }
}
