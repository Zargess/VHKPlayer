using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Enums;

namespace VHKPlayer.Utility {
    public class PathHandler {
        public static string[] SplitPath(string path) {
            var res = new string[0];
            if (path.Contains(@"\")) {
                const string splitcode = @"\";
                var splitchar = splitcode.ToCharArray()[0];
                res = path.Split(splitchar);
            } else if (path.Contains("/")) {
                res = path.Split('/');
            }
            return res;
        }

        public static string CombinePaths(string path1, string path2) {
            return Path.Combine(path1, path2);
        }

        public static string AbsolutePath(string relativepath) {
            //relativepath = relativepath.ToLower();
            //var root = App.ConfigService.GetString("root");
            //if (Directory.Exists(relativepath)) return relativepath;
            //if (File.Exists(relativepath)) return relativepath;
            //if (relativepath.Contains(@"root\")) relativepath = relativepath.Replace(@"root\", "");
            //var path = CombinePaths(root, relativepath);
            //return path.ToLower();
            return "";
        }

        public static FileType GetFileType(string filename) {
            filename = filename.ToLower();
            var temp = filename.Split('.');
            var extension = temp[temp.Length - 1];

            var pics = App.VHKPlayer.FolderConfigService.GetString("supportedPicture").Split(';').ToList();
            var vids = App.VHKPlayer.FolderConfigService.GetString("supportedVideo").Split(';').ToList();
            var mus = App.VHKPlayer.FolderConfigService.GetString("supportedMusic").Split(';').ToList();
            var inf = App.VHKPlayer.FolderConfigService.GetString("supportedInfo").Split(';').ToList();

            if (pics.Contains(extension)) {
                return FileType.Picture;
            }

            if (vids.Contains(extension)) {
                return FileType.Video;
            }

            if (mus.Contains(extension)) {
                return FileType.Music;
            }

            if (inf.Contains(extension)) {
                return FileType.Info;
            }

            return FileType.Unsupported;
        }

        public static string GetNameWithoutExtension(string name) {
            var temp = name.Split('.');
            var res = "";

            for (int i = 0; i < temp.Length - 1; i++) {
                res += temp[i];
            }

            return res;
        }

        public static string GetFileName(string path) {
            try {
                return Path.GetFileName(path);
            } catch (Exception) {
                return "";
            }
        }

        public static string GetSource(string path) {
            try {
                return Path.GetFileName(Path.GetDirectoryName(path));
            } catch (Exception) {
                return "";
            }
        }

        public static string GetPath(string path) {
            if (!File.Exists(path)) return path;
            var temp = PathHandler.SplitPath(path);
            return temp.Length > 1 ? path : Path.Combine(Environment.CurrentDirectory, path);
        }

    }
}
