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
                var playlist = GeneralFunctions.ConstructPlayList(constructionString);
                if (playlist == null) continue;
                content.Add(playlist);
            }
        }
    }
}
