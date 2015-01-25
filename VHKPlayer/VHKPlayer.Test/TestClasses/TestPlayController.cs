using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Test.TestClasses {
    public class TestPlayController : IPlayController {
        public IFile _video;
        public IFile _picture;
        public IFile _music;
        public string _action = "";

        public IStatistics Stats { get; private set; }

        public void Mute(FileType type) {
            _action = "mute";
        }

        public void Pause(FileType type) {
            _action = "pause";
        }

        public void Play(FileType type) {
            _action = "play";
        }

        public void Resume(FileType type) {
            _action = "resume";
        }

        public void Update(IFile file) {
            switch (file.Type) {
                case FileType.Music:
                    _music = file;
                    break;
                case FileType.Picture:
                    _picture = file;
                    break;
                case FileType.Video:
                    _video = file;
                    break;
            }
        }

        public void Stop(FileType type) {
            _action = "stop";
        }
        public bool Test { get; set; }
        public void ShowStats(IPlayer currentPlayable) {
            Stats = currentPlayable.Stats;
            Test = Stats == null;
        }
    }
}
