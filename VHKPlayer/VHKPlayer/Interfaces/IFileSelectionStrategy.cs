using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;

namespace VHKPlayer.Interfaces {
    public interface IFileSelectionStrategy {
        Queue<IFile> SelectFiles(IPlayable playable, PlayType type);
    }
}
