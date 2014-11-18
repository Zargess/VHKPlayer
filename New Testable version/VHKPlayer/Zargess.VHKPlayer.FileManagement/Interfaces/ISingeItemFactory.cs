using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement.Interfaces {
    public interface ISingeItemFactory {
        ILoadingStrategy CreateLoadingStrategy();
    }
}
