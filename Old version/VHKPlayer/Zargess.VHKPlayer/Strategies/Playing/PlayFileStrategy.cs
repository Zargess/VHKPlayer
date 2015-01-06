using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Playing {
    public class PlayFileStrategy : IPlayFileStrategy {
        /// <summary>
        /// Plays music and videos.
        /// Precondition FileType must be either Music or Video
        /// </summary>
        /// <param name="file">The file that should be played</param>
        /// <param name="type">The wanted PlayType</param>
        public void Play(IFile file, PlayType type) {
            App.PlayManager.SetCurrentFile(file);

            App.PlayManager.Play(file.Type);
        }
    }
}
