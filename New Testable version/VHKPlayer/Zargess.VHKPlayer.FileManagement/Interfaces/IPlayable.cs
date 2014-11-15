using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public interface IPlayable {
        string Name { get; }
        ILoadingStrategy LoadingStrategy { get; }
        ObservableCollection<IFile> Content { get; }
        Queue<IFile> Play();
    }
}