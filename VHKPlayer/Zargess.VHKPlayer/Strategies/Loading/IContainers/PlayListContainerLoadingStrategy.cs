using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Factories.IPlayLists;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Strategies.Loading.IContainers {
    public class PlayListContainerLoadingStrategy : ILoadingStrategy<IPlayable> {
        public void Load(ICollection<IPlayable> content) {
            var constructionStrings = App.ConfigService.GetString("playLists").Split(',');
            foreach (var constructionString in constructionStrings) {
                var type = GeneralFunctions.ConstructElements(constructionString).Last().ToLower();
                switch (type) {
                    case "allfilessorted":
                        content.Add(new PlayList(new AllFilesSortedPlayListFactory(constructionString)));
                        break;
                    case "iteratedfolder":
                        content.Add(new PlayList(new IteratedFolderPlayListFactory(constructionString)));
                        break;
                }
            }
        }
    }
}
