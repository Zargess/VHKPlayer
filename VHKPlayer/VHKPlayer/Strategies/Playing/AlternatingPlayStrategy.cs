using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Strategies.Playing {
    public class AlternatingPlayStrategy : IPlayStrategy {
        private IPlayStrategy _fileStrategy;
        private IPlayStrategy _playerStatStrategy;
        private IPlayStrategy _currentStrategy;
        private IPlayStrategy _autoplayliststrategy;

        public AlternatingPlayStrategy(IPlayStrategy filestrategy, IPlayStrategy playstatstrategy, IPlayStrategy autoplayliststrategy) {
            _fileStrategy = filestrategy;
            _playerStatStrategy = playstatstrategy;
            _autoplayliststrategy = autoplayliststrategy;
        }

        public void Play(IVideoPlayer videoplayer, Queue<IFile> queue, IPlayable playable, PlayType type) {
            SetCurrent(queue, type);
            _currentStrategy.Play(videoplayer, queue, playable, type);
        }

        // TODO : Test this new strategy and consider making a better way of getting the viewmodel
        private void SetCurrent(Queue<IFile> queue, PlayType type) {
            if (queue.Count == 0 && App.ViewModel.AutoPlayListEnabled) _currentStrategy = _autoplayliststrategy;
            if (type == PlayType.PlayerStat) _currentStrategy = _playerStatStrategy;
            else _currentStrategy = _fileStrategy;
        }
    }
}
