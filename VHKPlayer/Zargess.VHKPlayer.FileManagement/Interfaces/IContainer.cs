using System.Collections.ObjectModel;

namespace Zargess.VHKPlayer.FileManagement.Interfaces {
    public interface IContainer {
        ObservableCollection<IPlayable> Content { get; }
        string Name { get; }

        void Load();
    }
}
