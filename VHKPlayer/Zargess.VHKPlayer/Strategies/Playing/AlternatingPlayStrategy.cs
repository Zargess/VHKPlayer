using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Playing {
    public class AlternatingPlayStrategy : IPlayStrategy {
        private IPlayStrategy _musicStrategy;
        private IPlayStrategy _playerstatStrategy;
        private IPlayStrategy _viewableStrategy;
        private IPlayStrategy _current;

        public void Play(IPlayController controller, IPlayable playable, PlayType type) {
            DisernState(type);
            _current.Play(controller, playable, type);
        }

        private void DisernState(PlayType type) {
            if (type == PlayType.Music) _current = _musicStrategy;
            else if (type == PlayType.PlayerStat) _current = _playerstatStrategy;
            else _current = _viewableStrategy;
        }
    }
}
