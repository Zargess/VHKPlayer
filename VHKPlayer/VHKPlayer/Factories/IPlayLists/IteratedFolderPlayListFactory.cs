using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;
using VHKPlayer.Interfaces.Factories;
using VHKPlayer.Models;
using VHKPlayer.Strategies.Loading.PlayLists;
using VHKPlayer.Strategies.Selection.PlayLists;
using VHKPlayer.Utility;

namespace VHKPlayer.Factories.IPlayLists {
    public class IteratedFolderPlayListFactory : IPlayListFactory {
        private string[] Elements { get; set; }

        /*
        Precondition: constructionString must be a string seperated into 2 parts by ;
        */
        public IteratedFolderPlayListFactory(string constructionString) {
            Elements = GeneralFunctions.ConstructElements(constructionString);
        }

        public string CreateName() {
            return Elements[0];
        }

        public IFolder CreateFolder() {
            return new FolderNode(GeneralFunctions.AbsolutePath(Elements[1]));
        }

        public ILoadingStrategy<IFile> CreateLoadingStrategy() {
            return new FolderLoadingStrategy(CreateFolder());
        }

        public IFileSelectionStrategy CreateSelectionStrategy() {
            return new IteratedFileSelectionStrategy();
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
