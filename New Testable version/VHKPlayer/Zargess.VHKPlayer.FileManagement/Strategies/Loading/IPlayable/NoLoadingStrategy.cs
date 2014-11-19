using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Loading.IPlayable {
    public class NoLoadingStrategy : ILoadingStrategy {
        public void Load(ICollection<IFile> content) {}
    }
}
