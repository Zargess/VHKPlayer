using System;
using System.IO;
using System.Linq;
using Zargess.VHKPlayer.SettingsManager;
using Zargess.VHKPlayer.UtilFunctions;
using Zargess.VHKPlayer.FileManagement.Strategies.Loading;
using Zargess.VHKPlayer.FileManagement.Strategies.Selection;

namespace Zargess.VHKPlayer.FileManagement {
    public class FileNode : IFile {
        public string FullPath { get; private set; }
        public string Name { get; private set; }
        public string Source { get; private set; }
        public FileType Type { get; private set; }

        public FileNode(string path) {
            Name = GetFileName(path);
            FullPath = GetPath(path);
            Source = GetSource(path);
            Type = GetFileType();
        }

        private string GetFileName(string path) {
            try {
                return Path.GetFileName(path);
            } catch (Exception) {
                return "";
            }
        }

        private string GetSource(string path) {
            try {
                return Path.GetFileName(Path.GetDirectoryName(path));
            } catch (Exception) {
                return "";
            }
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
            if (!File.Exists(path)) return path;
            var temp = PathHandler.SplitPath(path);
            return temp.Length > 1 ? path : Path.Combine(Environment.CurrentDirectory, path);
        }

        public bool Exists() {
            return File.Exists(FullPath);
        }
    }
}