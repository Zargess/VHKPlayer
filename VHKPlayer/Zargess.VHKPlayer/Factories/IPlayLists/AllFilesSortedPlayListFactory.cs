using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Strategies.Loading.IPlayables;
using Zargess.VHKPlayer.Strategies.Selection.IPlayLists;
using Zargess.VHKPlayer.Strategies.Utils;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Factories.IPlayLists {
    public class AllFilesSortedPlayListFactory : IPlayListFactory {
        private string[] Elements { get; set; }

        /*
        Precondition: constructionString must be a string seperated into 3 parts by ;
        */
        public AllFilesSortedPlayListFactory(string constructionString) {
            Elements = GeneralFunctions.ConstructElements(constructionString);
        }

        public string CreateName() {
            return Elements[0];
        }

        public IFolder CreateFolder() {
            return new FolderNode(PathHandler.AbsolutePath(Elements[1]));
        }

        public ILoadingStrategy<IFile> CreateLoadingStrategy() {
            return new SortedLoadingStrategy(GeneralFunctions.StringToInteger(Elements[2]), CreateFolder());
        }

        public IFileSelectionStrategy CreateSelectionStrategy() {
            return new AllFilesSelectionStrategy(new GeneralQueuePeekStrategy());
        }

        public bool CreateRepeat() {
            var res = false;
            bool.TryParse(Elements[3], out res);
            return res;
        }
    }
}
