using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement;
using Zargess.VHKPlayer.FileManagement.Factories.PlayList;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.UtilFunctions;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Loading.Container.IPlayLists {
    public class PlayListContainerLoadingStrategy : ILoadingStrategy<IPlayable> {
        public void Load(ICollection<IPlayable> content) {
            var constructionStrings = SettingsManagement.Instance.GetStringSetting("playLists").Split(',');
            foreach (var constructionString in constructionStrings) {
                var type = GeneralFunctions.ConstructElements(constructionString).Last().ToLower();
                switch(type) {
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
