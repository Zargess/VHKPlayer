using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Strategies.Loading.IPlayables;
using Zargess.VHKPlayer.Strategies.Selection.IPlayLists;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Factories.IPlayLists {
    public class AllFilesFolderPlayListFactory : IPlayListFactory {
        private string[] Elements { get; set; }

        public AllFilesFolderPlayListFactory(string constructionString) {
            Elements = GeneralFunctions.ConstructElements(constructionString);
        }

        public IFolder CreateFolder() {
            return new FolderNode(Elements[1]);
        }

        public ILoadingStrategy<IFile> CreateLoadingStrategy() {
            return new FolderLoadingStrategy(CreateFolder());
        }

        public string CreateName() {
            return Elements[0];
        }

        public IFileSelectionStrategy CreateSelectionStrategy() {
            return new AllFilesSelectionStrategy();
        }
    }
}
