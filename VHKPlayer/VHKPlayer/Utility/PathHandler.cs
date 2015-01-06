﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
