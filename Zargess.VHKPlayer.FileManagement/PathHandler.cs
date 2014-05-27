using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
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

        public static string RemovePathFromNode(Node org, Node toRemove) {
            var res = "";
            res = org.FullPath;
            res = res.Replace(toRemove.FullPath, "");
            return res;
        }
    }
}
