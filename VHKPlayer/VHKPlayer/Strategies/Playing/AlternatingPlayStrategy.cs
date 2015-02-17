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
        private IPlayStrategy _autoplaylistStrategy;
        private IPlayStrategy _donothingStrategy;

        public AlternatingPlayStrategy(IPlayStrategy filestrategy, IPlayStrategy playstatstrategy, IPlayStrategy autoplayliststrategy, IPlayStrategy donothingstrategy) {
            _fileStrategy = filestrategy;
            _playerStatStrategy = playstatstrategy;
            _autoplaylistStrategy = autoplayliststrategy;
            _donothingStrategy = donothingstrategy;
        }

        public void Play(IVideoPlayer videoplayer, Queue<IFile> queue, IPlayable playable, PlayType type) {
            SetCurrent(queue, playable, type);
            _currentStrategy.Play(videoplayer, queue, playable, type);
        }

        // TODO : Test this new strategy and consider making a better way of getting the viewmodel
        // TODO : Check if this even works
        private void SetCurrent(Queue<IFile> queue, IPlayable playable, PlayType type) {
            var next = playable.HintNext(queue);
            var enabled = App.ViewModel.AutoPlayListEnabled;
            var empty = queue.Count == 0;

            if (next == null && enabled && empty) _currentStrategy = _autoplaylistStrategy;
            else if (type == PlayType.PlayerStat) _currentStrategy = _playerStatStrategy;
            else if (!empty) _currentStrategy = _fileStrategy;
            else _currentStrategy = _donothingStrategy;
        }
    }
}
