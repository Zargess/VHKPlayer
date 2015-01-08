using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Test.Utility {
    public class Constants {
        public static string GithubPath {
            get {
                return Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\Github";
            }
        }

        public static string RootFolderPath {
            get {
                return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Dropbox\Programmering\C#\damer 2013-2014";
            }
        }
    }
}
