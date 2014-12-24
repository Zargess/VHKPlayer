using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Strategies.Selection.IPlayManager {
    public class Auto10SekSelectionStrategy : IFileSelectionStrategy {
        private IPlayList Auto10SekPlayList { get; set; }

        public Auto10SekSelectionStrategy() {
            App.ConfigService.PropertyChanged += (sender, ee) => Auto10SekPlayList = GeneralFunctions.ConstructPlayList(App.ConfigService.GetString("auto10SekPlayList"));
            Auto10SekPlayList = GeneralFunctions.ConstructPlayList(App.ConfigService.GetString("auto10SekPlayList"));
        }

        public IFile HintNext(Queue<IFile> q, IPlayable p, PlayType type) {
            return Auto10SekPlayList.SelectionStrategy.HintNext(q, p, type);
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            return Auto10SekPlayList.Play(type);
        }
    }
}
