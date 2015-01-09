using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Utility {
    public class GeneralFunctions {
        public static int StringToInteger(string s) {
            int n;
            int.TryParse(s, out n);
            return n;
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
    }
}
