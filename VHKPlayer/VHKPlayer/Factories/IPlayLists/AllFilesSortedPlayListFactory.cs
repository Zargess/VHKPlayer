using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;
using VHKPlayer.Interfaces.Factories;
using VHKPlayer.Models;
using VHKPlayer.Strategies.Loading.PlayLists;
using VHKPlayer.Strategies.Peeking;
using VHKPlayer.Strategies.Selection.PlayLists;
using VHKPlayer.Utility;

namespace VHKPlayer.Factories.IPlayLists {
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
            return new FolderNode(GeneralFunctions.AbsolutePath(Elements[1]));
        }

        public ILoadingStrategy<IFile> CreateLoadingStrategy() {
            return new SortedLoadingStrategy(GeneralFunctions.StringToInteger(Elements[2]), CreateFolder());
        }

        public IFileSelectionStrategy CreateSelectionStrategy() {
            return new AllFilesSelectionStrategy(new NextInQueuePeekStrategy());
        }

        public bool CreateRepeat() {
            return StringToBool(Elements[3]);
        }

        public bool CreateHasAudio() {
            return StringToBool(Elements[4]);
        }

        private bool StringToBool(string s) {
            var res = false;
            bool.TryParse(s, out res);
            return res;
        }
    }
}
