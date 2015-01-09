using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;

namespace VHKPlayer.Utility {
    public class PathHandler {
        public static FileType GetFileType(string extension) {
            var music = Settings.FolderConfig.GetString("supportedMusic").Split(';');
            var picture = Settings.FolderConfig.GetString("supportedPicture").Split(';');
            var video = Settings.FolderConfig.GetString("supportedVideo").Split(';');
            var info = Settings.FolderConfig.GetString("supportedInfo").Split(';');

            if (music.Contains(extension)) return FileType.Music;
            if (picture.Contains(extension)) return FileType.Picture;
            if (video.Contains(extension)) return FileType.Video;
            if (info.Contains(extension)) return FileType.Info;
            return FileType.Unsupported;
        }
    }
}
