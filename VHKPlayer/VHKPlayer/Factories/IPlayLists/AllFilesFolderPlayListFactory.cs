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
            return new AllFilesSelectionStrategy(new NextInQueuePeekStrategy());
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
