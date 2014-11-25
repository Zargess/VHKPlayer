using System.Collections.Generic;

namespace Zargess.VHKPlayer.FileManagement.Interfaces {
    public interface IFileSelectionStrategy {
        Queue<IFile> SelectFiles(IPlayable playable);
    }
}
