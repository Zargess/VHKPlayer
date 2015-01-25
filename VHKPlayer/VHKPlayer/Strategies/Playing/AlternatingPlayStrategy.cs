using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Strategies.Playing {
    public class AlternatingPlayStrategy : IPlayStrategy {
        private IPlayStrategy _fileStrategy;
        private IPlayStrategy _playerStatStrategy;
        private IPlayStrategy _currentStrategy;

        public AlternatingPlayStrategy(IPlayStrategy filestrategy, IPlayStrategy playstatstrategy) {
            _fileStrategy = filestrategy;
            _playerStatStrategy = playstatstrategy;
        }

        public void Play(IVideoPlayer videoplayer, Queue<IFile> queue, IPlayable playable, PlayType type) {
            SetCurrent(type);
            _currentStrategy.Play(videoplayer, queue, playable, type);
        }

        private void SetCurrent(PlayType type) {
            if (type == PlayType.PlayerStat) _currentStrategy = _playerStatStrategy;
            else _currentStrategy = _fileStrategy;
        }
    }
}
