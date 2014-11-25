using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.FileManagement.Strategies.Loading.IPlayable;
using Zargess.VHKPlayer.FileManagement.Strategies.Selection;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement.Factories.PlayList {
    public class AllFilesNoLoadingFactory : IPlayListFactory {
        private string[] Elements { get; set; }

        public AllFilesNoLoadingFactory(string constructionString) {
            Elements = GeneralFunctions.ConstructElements(constructionString);
        }

        /*
        Precondition: constructionString must be a string seperated into 2 parts by ;
        */
        public IFolder CreateFolder() {
            return new FolderNode(PathHandler.AbsolutePath(Elements[1]));
        }

        public ILoadingStrategy CreateLoadingStrategy() {
            return new NoLoadingStrategy();
        }

        public string CreateName() {
            return Elements[0];
        }

        public IFileSelectionStrategy CreateSelectionStrategy() {
            return new AllFilesSelectionStrategy();
        }
    }
}