using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;

namespace VHKPlayer.Utility {
    public class PathHandler {
        public static FileType GetFileType(string extension) {
            if (Settings.SupportedMusic.Contains(extension)) return FileType.Music;
            if (Settings.SupportedPicture.Contains(extension)) return FileType.Picture;
            if (Settings.SupportedVideo.Contains(extension)) return FileType.Video;
            if (Settings.SupportedInfo.Contains(extension)) return FileType.Info;
            return FileType.Unsupported;
        }
    }
}