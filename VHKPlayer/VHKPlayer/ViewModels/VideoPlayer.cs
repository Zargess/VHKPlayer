using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.ViewModels {
    public class VideoPlayer : IVideoPlayer {

        public VideoPlayer(ISettings folderSetting) {
            Settings.FolderConfig = new GlobalConfigService(folderSetting);
        }
    }
}
