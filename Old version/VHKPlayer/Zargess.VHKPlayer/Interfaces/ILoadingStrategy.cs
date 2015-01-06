using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Interfaces {
    public interface ILoadingStrategy<T> {
        void Load(ICollection<T> content);
    }
}
