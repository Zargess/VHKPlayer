using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Exceptions;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Strategies.Selection.Players {
    public class TypeDependendSelectionStrategy : IFileSelectionStrategy {
        private IFileSelectionStrategy _pictureStrategy;
        private IFileSelectionStrategy _videoStrategy;
        private IFileSelectionStrategy _statStrategy;
        private IFileSelectionStrategy _current;

        public TypeDependendSelectionStrategy(IFileSelectionStrategy pictureStrategy, IFileSelectionStrategy videoStrategy, IFileSelectionStrategy statStrategy) {
            _pictureStrategy = pictureStrategy;
            _videoStrategy = videoStrategy;
            _statStrategy = statStrategy;
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            SetCurrentStrategy(type);
            return _current.SelectFiles(playable, type);
        }

        private void SetCurrentStrategy(PlayType type) {
            if (type == PlayType.PlayerPicture) _current = _pictureStrategy;
            else if (type == PlayType.PlayerVideo) _current = _videoStrategy;
            else if (type == PlayType.PlayerStat) _current = _statStrategy;
            else throw new InvalidTypeException("Wrong playtype for play call");
        }

        public IFile HintNext(IPlayable playable, Queue<IFile> queue) {
            if (_current == null) return null;
            return _current.HintNext(playable, queue);
        }
    }
}
