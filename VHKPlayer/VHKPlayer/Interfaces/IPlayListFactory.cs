using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interfaces.Factories {
    public interface IPlayListFactory {
        string CreateName();
        IFolder CreateFolder();
        IFileSelectionStrategy CreateSelectionStrategy();
        ILoadingStrategy<IFile> CreateLoadingStrategy();
        bool CreateRepeat();
        bool CreateHasAudio();
    }
}
