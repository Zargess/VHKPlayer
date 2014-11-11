using System;
using System.IO;
using System.Linq;
using Zargess.VHKPlayer.SettingsManager;

namespace Zargess.VHKPlayer.FileManagement {
    public class FileImpl : IFile {
        public string FullPath { get; private set; }
        public string Name { get; private set; }
        public string Source { get; private set; }
        public FileType Type { get; private set; }

        public FileImpl(string path) {
            Name = Path.GetFileName(path);
            FullPath = GetPath(path);
            Source = Path.GetFileName(Path.GetDirectoryName(path));
            Type = GetFileType();
        }

        private FileType GetFileType() {
            var fullPath = FullPath.ToLower();
            var temp = fullPath.Split('.');
            var extension = temp[temp.Length - 1];
            var pics = SettingsManagement.GetStringSetting("supportedPicture").Split(';').ToList();
            var vids = SettingsManagement.GetStringSetting("supportedVideo").Split(';').ToList();
            var mus = SettingsManagement.GetStringSetting("supportedMusic").Split(';').ToList();

            if (pics.Contains(extension)) {
                return FileType.Picture;
            }

            if (vids.Contains(extension)) {
                return FileType.Video;
            }

            if (mus.Contains(extension)) {
                return FileType.Music;
            }

            return FileType.Unsupported;
        }

        private string GetPath(string path) {
            var temp = path.Split('\\');
            return temp.Length > 1 ? path : Path.Combine(Environment.CurrentDirectory, path);
        }

        public bool Exists() {
            return File.Exists(FullPath);
        }
    }
}