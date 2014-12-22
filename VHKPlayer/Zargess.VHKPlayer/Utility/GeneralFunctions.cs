using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.Enums;
using Zargess.VHKPlayer.Factories.IPlayLists;
using Zargess.VHKPlayer.Interfaces;
using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.Utility {
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

        public static IPlayList ConstructPlayList(string constructionString) {
            var type = ConstructElements(constructionString).Last().ToLower();
            switch (type) {
                case "allfilesfolder":
                    return new PlayList(new AllFilesFolderPlayListFactory(constructionString));
                case "allfilesnoloading":
                    return new PlayList(new AllFilesNoLoadingFactory(constructionString));
                case "allfilessorted":
                    return new PlayList(new AllFilesSortedPlayListFactory(constructionString));
                case "iteratedfolder":
                    return new PlayList(new IteratedFolderPlayListFactory(constructionString));
                case "iteratednoloading":
                    return new PlayList(new IteratedNoLoadingFactory(constructionString));
                case "iteratedsorted":
                    return new PlayList(new IteratedSortedPlayListFactory(constructionString));
            }

            return null;
        }

        public static PlayType GetPlayType(string controlName) {
            switch (controlName) {
                case "PlayList":
                    return PlayType.PlayList;
                case "PlayerPicture":
                    return PlayType.PlayerPic;
                case "PlayerVideo":
                    return PlayType.PlayerVid;
                case "PlayerVideoStat":
                    return PlayType.PlayerStat;
                default:
                    return PlayType.Standard;
            }
        }
    }
}
