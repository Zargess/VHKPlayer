using System.Collections.Generic;
using System.Collections.ObjectModel;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Interfaces {
    public interface ILoadingStrategy<T> {
        void Load(ICollection<T> content);
    }
}