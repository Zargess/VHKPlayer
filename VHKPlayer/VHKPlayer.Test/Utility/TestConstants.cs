using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Test.Utility {
    public class TestConstants {
        public static string GithubPath {
            get {
                var s = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Github\VHKPlayer";
				if (!Directory.Exists(s)) s = @"D:\Github\VHKPlayer";
                return s.ToLower();
            }
        }

        public static string RootFolderPath {
            get {
                var s = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Dropbox\Programmering\C#\vhk";
                if (!Directory.Exists(s)) s = @"D:\Dropbox\Programmering\C#\vhk";
                return s.ToLower();
            }
        }
    }
}