using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Test.Utility {
    public class Constants {
        private static bool _exists = File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Dropbox\Programmering\C#\vhk");
        public static string GithubPath {
            get {
                var s = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\Github";
                if (!_exists) s = @"D:\Github\";
                return s.ToLower();
            }
        }

        public static string RootFolderPath {
            get {
                var s = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Dropbox\Programmering\C#\vhk";
                if (!_exists) s = @"D:\Dropbox\Programmering\C#\vhk";
                return s.ToLower();
            }
        }
    }
}