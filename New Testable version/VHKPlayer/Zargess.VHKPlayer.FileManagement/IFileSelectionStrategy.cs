using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zargess.VHKPlayer.FileManagement {
    public interface IFileSelectionStrategy {
        Queue<IFile> SelectFiles(IPlayable p);
    }
}
