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
    public class IteratedNoLoadingFactory : IPlayListFactory {
        private string[] Elements { get; set; }

        /*
        Precondition: constructionString must be a string seperated into 2 parts by ;
        */
        public IteratedNoLoadingFactory(string constructionString) {
            Elements = GeneralFunctions.ConstructElements(constructionString);
        }

        public IFolder CreateFolder() {
            return new FolderNode(PathHandler.AbsolutePath(Elements[1]));
        }

        public ILoadingStrategy<IFile> CreateLoadingStrategy() {
            return new NoLoadingStrategy();
        }

        public string CreateName() {
            return Elements[0];
        }

        public IFileSelectionStrategy CreateSelectionStrategy() {
            return new IteratedFileSelectionStrategy(new IteratedQueuePeekStrategy());
        }

        public bool CreateRepeat() {
            return StringToBool(Elements[2]);
        }

        public bool CreateHasAudio() {
            return StringToBool(Elements[3]);
        }

        private bool StringToBool(string s) {
            var res = false;
            bool.TryParse(s, out res);
            return res;
        }
    }
}
