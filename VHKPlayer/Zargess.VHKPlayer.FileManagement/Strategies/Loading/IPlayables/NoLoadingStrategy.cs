using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Interfaces;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Loading.IPlayables {
    public class NoLoadingStrategy : ILoadingStrategy<IFile> {
        public void Load(ICollection<IFile> content) {}
    }
}
