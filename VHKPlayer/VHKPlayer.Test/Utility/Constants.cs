using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Test.Utility {
    public class Constants {
        public static string GithubPath {
            get {
                var s = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\Github";
                return s.ToLower();
            }
        }

        public static string RootFolderPath {
            get {
                var s = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Dropbox\Programmering\C#\vhk";
                return s.ToLower();
            }
        }
    }
}
