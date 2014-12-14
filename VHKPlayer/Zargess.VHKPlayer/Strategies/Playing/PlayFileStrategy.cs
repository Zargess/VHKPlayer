using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Playing {
    public class PlayFileStrategy : IPlayStrategy {
        public void Play(IFile file, PlayType type) {
            if (file.Type != FileType.Music || file.Type != FileType.Video) return;
            if (file.Type == FileType.Video) {
                App.PlayerViewModel.CurrentVideoFile = file.Clone();
                App.PlayerViewModel.PlayVideo();
            } else if (file.Type == FileType.Music) {
                App.PlayerViewModel.CurrentMusicFile = file.Clone();
                App.PlayerViewModel.PlayMusic();
            }
        }
    }
}
