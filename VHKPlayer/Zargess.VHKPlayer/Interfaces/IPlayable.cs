using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayable {
        string Name { get; }
        ObservableCollection<IFile> Content { get; }
        Queue<IFile> Play(PlayType pt);
    }
}