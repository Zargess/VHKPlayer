using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement {
    public class PlayableCreator {
        public static List<IPlayable> CreatePlayables(string type, string[] args) {
            List<IPlayable> res = null;
            switch (type) {
                case "files":
                    if (args.Length == 1) {
                        res = FolderContent(args[0]);
                    }
                    break;
            }
            return res;
        }

        private static List<IPlayable> FolderContent(string p) {
            var content = Directory.GetFiles(p);
            var temp = content.Select(x => new FileNode(x)).ToList();

            return temp.Cast<IPlayable>().ToList();
        }
    }
}
