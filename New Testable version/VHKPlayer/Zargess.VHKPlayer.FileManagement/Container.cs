using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class Container : IContainer {
        public ObservableCollection<IPlayable> Content { get; private set; }

        public string Name {
            get {
                throw new NotImplementedException();
            }
        }
    }
}
