using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Playing.Playable {
    public class PlayMusicPlayableStrategy : IPlayablePlayStrategy {
        public void Play(IPlayManager manager, IPlayable playable, PlayType type) {
            var files = playable.Play(type);
            if (files.Count != 1) return;
            var file = files.Dequeue();
            manager.SetCurrentFile(file);
            manager.PlayingMusic = true;
            manager.Play(file.Type);
        }
    }
}
