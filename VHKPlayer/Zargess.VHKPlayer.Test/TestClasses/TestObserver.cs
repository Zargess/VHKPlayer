
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Test.TestClasses {
    public class TestObserver : IPlayObserver {
        public IFile _video;
        public IFile _picture;
        public IFile _music;
        public string _action = "";

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

        public void SetCurrentFile(IFile file) {
            switch(file.Type) {
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
    }
}
