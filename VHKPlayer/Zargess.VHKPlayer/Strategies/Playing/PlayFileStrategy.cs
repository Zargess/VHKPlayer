using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Playing {
    public class PlayFileStrategy : IPlayStrategy {
        /// <summary>
        /// Plays music and videos.
        /// Precondition FileType must be either Music or Video
        /// </summary>
        /// <param name="file">The file that should be played</param>
        /// <param name="type">The wanted PlayType</param>
        public void Play(IFile file, PlayType type) {
            if (file.Type == FileType.Video) {
                App.PlayerViewModel.CurrentVideoFile = file.Clone();
            } else if (file.Type == FileType.Music) {
                App.PlayerViewModel.CurrentMusicFile = file.Clone();
            }

            App.PlayerViewModel.Manager.Play(file.Type);
        }
    }
}
