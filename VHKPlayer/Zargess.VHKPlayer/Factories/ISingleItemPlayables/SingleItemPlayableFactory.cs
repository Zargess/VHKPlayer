using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Strategies.Loading.IPlayables;
using Zargess.VHKPlayer.Strategies.Selection.ISingleItemPlayables;

namespace Zargess.VHKPlayer.Factories.ISingleItemPlayables {
    public class SingleItemPlayableFactory : ISingleItemPlayableFactory {

        public IFile File { get; private set; }

        public SingleItemPlayableFactory(IFile file) {
            File = file;
        }

        public ILoadingStrategy<IFile> CreateLoadingStrategy() {
            return new FileLoadingStrategy(File.FullPath);
        }

        public IFileSelectionStrategy CreateSelectionStrategy() {
            return new SingleItemPlayableSelectionStrategy();
        }
    }
}
