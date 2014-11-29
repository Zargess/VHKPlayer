namespace Zargess.VHKPlayer.FileManagement.Interfaces {
    public interface ISingeItemFactory {
        ILoadingStrategy<IFile> CreateLoadingStrategy();
    }
}
