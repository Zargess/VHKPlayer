using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Controllers {
    public class PlayController : IPlayController {

        public PlayController(MediaElement viewer, MediaElement audio, Image viewport, bool allowAudio, bool allowStat, ISoundStrategy soundStrategy) {

        }

        public void Mute(FileType type) {
            throw new NotImplementedException();
        }

        public void Pause(FileType type) {
            throw new NotImplementedException();
        }

        public void Play(FileType type) {
            throw new NotImplementedException();
        }

        public void Resume(FileType type) {
            throw new NotImplementedException();
        }

        public void ShowStats(IPlayer player) {
            throw new NotImplementedException();
        }

        public void Stop(FileType type) {
            throw new NotImplementedException();
        }

        public void Update(IFile file) {
            throw new NotImplementedException();
        }
    }
}
