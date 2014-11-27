using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Zargess.VHKPlayer.FileManagement.Interfaces {
    public interface ICompositeContainer {
        ObservableCollection<IContainer> Content { get; }
        void Load();
    }
}
