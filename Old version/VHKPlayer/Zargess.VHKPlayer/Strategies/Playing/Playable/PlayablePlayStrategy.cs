using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Playing.Playable {
    public class PlayablePlayStrategy : IPlayablePlayStrategy {
        private IPlayablePlayStrategy _musicPlayStrategy;
        private IPlayablePlayStrategy _viewablePlayStrategy;
        private IPlayablePlayStrategy _currentPlayStrategy;

        public PlayablePlayStrategy(IPlayablePlayStrategy music, IPlayablePlayStrategy viewable) {
            _musicPlayStrategy = music;
            _viewablePlayStrategy = viewable;
        }

        public void Play(IPlayManager manager, IPlayable playable, PlayType type) {
            SetCurrent(type);
            _currentPlayStrategy.Play(manager, playable, type);
        }

        private void SetCurrent(PlayType type) {
            if (type == PlayType.Music) _currentPlayStrategy = _musicPlayStrategy;
            else _currentPlayStrategy = _viewablePlayStrategy;
        }
    }
}
