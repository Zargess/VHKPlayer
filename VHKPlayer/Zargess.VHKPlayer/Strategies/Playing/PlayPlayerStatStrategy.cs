using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Interfaces;

namespace Zargess.VHKPlayer.Strategies.Playing {
    public class PlayPlayerStatStrategy : IPlayStrategy {
        public void Play(IFile file, PlayType type) {
            if (file.Type == FileType.Picture) {
                // TODO : Call ShowImageStrategy and handle stats
            }
            App.PlayManager.SetCurrentFile(file);
            App.PlayManager.Play(file.Type);
        }
    }
}
