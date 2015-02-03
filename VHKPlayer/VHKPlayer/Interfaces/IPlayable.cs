using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;

namespace VHKPlayer.Interfaces {
    public interface IPlayable {
        string Name { get; }
        bool Repeat { get; }
        ObservableCollection<IFile> Content { get; }
        Queue<IFile> Play(PlayType type);
        IFile HintNext(Queue<IFile> queue);
    }
}