using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Zargess.VHKPlayer.FileManagement {
    public interface ILoadingStrategy {
        void Load(ICollection<IFile> content);
    }
}