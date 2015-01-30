using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Test.Utility {
    public class Constants {
        public static string GithubPath {
            get {
                var s = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Github";
				if (!Directory.Exists(s)) s = @"D:\Github";
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