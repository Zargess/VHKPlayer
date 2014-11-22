using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement {
    public interface IFileSelectionStrategy {
        Queue<IFile> SelectFiles(IPlayable playable);
    }
}
