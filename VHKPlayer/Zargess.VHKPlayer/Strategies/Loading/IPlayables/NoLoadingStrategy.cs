using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Loading.IPlayables {
    public class NoLoadingStrategy : ILoadingStrategy<IFile> {
        public void Load(ICollection<IFile> content) { }
    }
}
