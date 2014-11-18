using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement.Factories {
    public interface IPlayListFactory {
        string CreateName();
        IFolder CreateFolder();
        IFileSelectionStrategy CreateSelectionStrategy();
        ILoadingStrategy CreateLoadingStrategy();
    }
}
