using Zargess.VHKPlayer.FileManagement.Strategies.Loading;
using Zargess.VHKPlayer.FileManagement.Strategies.Selection;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement.Factories.PlayList {
    public class IteratedSortedPlayListFactory : IPlayListFactory {
        private string[] Elements { get; set; }

        /*
        Precondition: constructionString must be a string seperated into 3 parts by ;
        */
        public IteratedSortedPlayListFactory(string constructionString) {
            Elements = GeneralFunctions.ConstructElements(constructionString);
        }

        public string CreateName() {
            return Elements[0];
        }

        public IFolder CreateFolder() {
            return new FolderNode(PathHandler.AbsolutePath(Elements[1]));
        }

        public ILoadingStrategy CreateLoadingStrategy() {
            return new SortedLoadingStrategy(GeneralFunctions.StringToInteger(Elements[2]), CreateFolder());
        }

        public IFileSelectionStrategy CreateSelectionStrategy() {
            return new IteratedFileSelectionStrategy();
        }
    }
}
