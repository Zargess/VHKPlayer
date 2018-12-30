using System.IO;
using System.Linq;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetStringSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility.FindFileType.Interfaces;

namespace VHKPlayer.Utility.FindFileType
{
    public class FindFileTypeStrategy : IFindFileTypeStrategy
    {
        private readonly IQueryProcessor _processor;

        public FindFileTypeStrategy(IQueryProcessor processor)
        {
            this._processor = processor;
        }

        public FileType FindType(string path)
        {
            var supportedVideo = _processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.SupportedVideoSettingName
            }).Split(';').ToList();
            var supportedAudio = _processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.SupportedAudioSettingName
            }).Split(';').ToList();
            var supportedPicture = _processor.Process(new GetStringSettingQuery()
            {
                SettingName = Constants.SupportedPictureSettingName
            }).Split(';').ToList();

            var extension = Path.GetExtension(path);

            if (supportedVideo.Contains(extension)) return FileType.Video;
            if (supportedAudio.Contains(extension)) return FileType.Audio;
            if (supportedPicture.Contains(extension)) return FileType.Picture;
            return FileType.Unsupported;
        }
    }
}