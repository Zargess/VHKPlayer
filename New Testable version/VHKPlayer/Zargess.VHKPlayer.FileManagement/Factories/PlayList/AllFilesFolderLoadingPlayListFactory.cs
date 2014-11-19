using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Strategies.Loading;
using Zargess.VHKPlayer.FileManagement.Strategies.Selection;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement.Factories.PlayList {
    public class AllFilesFolderLoadingPlayListFactory : IPlayListFactory {

        private string[] Elements { get; set; }

        /*
        Precondition: constructionString must be a string seperated into 2 parts by ;
        */
        public AllFilesFolderLoadingPlayListFactory(string contructionString) {
            Elements = GeneralFunctions.ConstructElements(contructionString);
        }

        public string CreateName() {
            return Elements[0];
        }

        public IFolder CreateFolder() {
            return new FolderNode(Elements[1]);
        }

        public ILoadingStrategy CreateLoadingStrategy() {
            return new FolderLoadingStrategy(CreateFolder());
        }

        public IFileSelectionStrategy CreateSelectionStrategy() {
            return new AllFilesSelectionStrategy();
        }
    }
}
