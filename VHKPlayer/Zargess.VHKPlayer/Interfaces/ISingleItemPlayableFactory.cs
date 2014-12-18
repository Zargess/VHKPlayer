using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface ISingleItemPlayableFactory {
        ILoadingStrategy<IFile> CreateLoadingStrategy();
        IFileSelectionStrategy CreateSelectionStrategy();
    }
}
