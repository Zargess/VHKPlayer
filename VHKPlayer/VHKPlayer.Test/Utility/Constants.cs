using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Test.Utility {
    public class Constants {
        public static string GetRootFolder() {
            var path = @"D:\Github";
            var path2 = @"C:\users\mfh\vhk";
            var res = "";
            var statfolder = "";
            var currentfolder = "";
            if (Directory.Exists(path)) {
                res = @"D:\Dropbox\Programmering\C#\damer 2013-2014";
                statfolder = @"D:\Dropbox\Programmering\C#\digimatch";
                currentfolder = @"D:\GitHub\VHKPlayer\Files for unit test";
            } else if (Directory.Exists(path2)) {
                res = @"c:\users\mfh\dropbox\programmering\c#\damer 2013-2014";
                statfolder = @"c:\users\mfh\dropbox\programmering\c#\digimatch";
                currentfolder = @"C:\Users\MFH\Documents\GitHub\VHKPlayer\Files for unit test";
            } else {
                res = @"C:\Users\Marcus\Dropbox\Programmering\C#\damer 2013-2014";
                statfolder = @"c:\users\marcus\Dropbox\Programmering\C#\digimatch";
                currentfolder = @"C:\Users\Marcus\Documents\GitHub\VHKPlayer\Files for unit test";
            }
            //App.ConfigService.Update("statsFolder", statfolder);
            Environment.CurrentDirectory = currentfolder;
            return res;
        }
    }
}
