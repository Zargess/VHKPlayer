using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zargess.VHKPlayer.Enums;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayable {
        string Name { get; }
        bool Repeat { get; set; }
        ObservableCollection<IFile> Content { get; }
        Queue<IFile> Play(PlayType pt);
    }
}