using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Interfaces {
    public interface IPlayable {
        string Name { get; }
        ObservableCollection<IFile> GetContent();
        int Size { get; }
        Queue<IFile> Play(PlayType pt);
    }
}