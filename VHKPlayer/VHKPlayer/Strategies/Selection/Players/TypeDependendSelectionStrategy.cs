using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Strategies.Selection.Players {
    public class TypeDependendSelectionStrategy : IFileSelectionStrategy {
        private IFileSelectionStrategy _pictureStrategy;
        private IFileSelectionStrategy _videoStrategy;
        private IFileSelectionStrategy _current;

        public TypeDependendSelectionStrategy(IFileSelectionStrategy pictureStrategy, IFileSelectionStrategy videoStrategy) {
            _pictureStrategy = pictureStrategy;
            _videoStrategy = videoStrategy;
        }

        public Queue<IFile> SelectFiles(IPlayable playable, PlayType type) {
            SetCurrentStrategy(type);
            return _current.SelectFiles(playable, type);
        }

        private void SetCurrentStrategy(PlayType type) {
            if (type == PlayType.PlayerPicture) _current = _pictureStrategy;
            else _current = _videoStrategy;
        }
    }
}
