using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;
using VHKPlayer.Exceptions;
using VHKPlayer.Factories.IPlayLists;
using VHKPlayer.Interfaces;
using VHKPlayer.Interfaces.Factories;
using VHKPlayer.Models;

namespace VHKPlayer.Utility {
    public class GeneralFunctions {
        public static int StringToInteger(string s) {
            int n;
            int.TryParse(s, out n);
            return n;
        }

        public static string[] ConstructElements(string s) {
            s = s.Replace("{", "");
            s = s.Replace("}", "");
            var res = s.Split(';');
            return res;
        }

        public static List<string> GetPlayerFolderPaths() {
            var res = new List<string>();

            res.Add(AbsolutePath(Settings.FolderConfig.GetString("playerFolderPicture")));
            res.Add(AbsolutePath(Settings.FolderConfig.GetString("playerFolderVideo")));
            res.Add(AbsolutePath(Settings.FolderConfig.GetString("playerFolderStatPicture")));
            res.Add(AbsolutePath(Settings.FolderConfig.GetString("playerFolderStatVideo")));
            res.Add(AbsolutePath(Settings.FolderConfig.GetString("playerFolderStatMusic")));

            return res;
        }

        public static string AbsolutePath(string relativepath) {
            relativepath = relativepath.ToLower();
            var root = Settings.FolderConfig.GetString("root");
            if (Directory.Exists(relativepath)) return relativepath;
            if (File.Exists(relativepath)) return relativepath;
            if (relativepath.Contains(@"root\")) relativepath = relativepath.Replace(@"root\", "");
            var path = Path.Combine(root, relativepath);
            return path.ToLower();
        }

        public static IPlayList ConstructPlayList(string v) {
            IPlayList res;
            var factory = FindPlayListFactory(v);
            res = new PlayList(factory);
            return res;
        }

        private static IPlayListFactory FindPlayListFactory(string v) {
            var elements = ConstructElements(v);
            switch (elements.Last()) {
                case "AllFilesFolder":
                    return new AllFilesFolderPlayListFactory(v);
                case "AllFilesSorted":
                    return new AllFilesSortedPlayListFactory(v);
                case "IteratedFolder":
                    return new IteratedFolderPlayListFactory(v);
                case "IteratedSorted":
                    return new IteratedSortedPlayListFactory(v);
                default:
                    throw new NoSuchPlayListTypeException("Playlisten du prøver at skabe bruger en ukendt type.");
            }
        }

        public static FileType GetFileType(string extension) {
            if (Settings.SupportedMusic.Contains(extension)) return FileType.Music;
            if (Settings.SupportedPicture.Contains(extension)) return FileType.Picture;
            if (Settings.SupportedVideo.Contains(extension)) return FileType.Video;
            if (Settings.SupportedInfo.Contains(extension)) return FileType.Info;
            return FileType.Unsupported;
        }
    }
}
