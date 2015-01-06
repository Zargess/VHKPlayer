using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Collections;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Strategies.Playing;
using Zargess.VHKPlayer.Strategies.Playing.Playable;
using Zargess.VHKPlayer.Strategies.Selection;
using Zargess.VHKPlayer.Strategies.Selection.IPlayManager;
using Zargess.VHKPlayer.Utility;

namespace Zargess.VHKPlayer.Factories.IPlayManagers {
    public class PlayManagerFactory : IPlayManagerFactory {
        private CustomQueue<IFile> _queue = new CustomQueue<IFile>();

        public IPlayList CreateAuto10SekPlayList() {
            return GeneralFunctions.ConstructPlayList(App.ConfigService.GetString("auto10SekPlayList"));
        }

        public List<IPlayObserver> CreateObserverList() {
            return new List<IPlayObserver>();
        }

        public IPlayablePlayStrategy CreatePlayablePlayStrategy() {
            return new PlayablePlayStrategy(new PlayMusicPlayableStrategy(), new ViewablePlayablePlayStrategy());
        }

        public IPlayFileStrategy CreatePlayFileStrategy() {
            return new GeneralPlayStrategy(new PlayFileStrategy(), new PlayPlayerStatStrategy());
        }

        public CustomQueue<IFile> CreateQueue() {
            return _queue;
        }

        public IFileSelectionStrategy CreateQueueEmptyStrategy() {
            return new GeneralManagerSelectionStrategy(_queue, new RepeatSelectionStrategy(), new Auto10SekSelectionStrategy(), new NullSelectionStrategy());
        }
    }
}
