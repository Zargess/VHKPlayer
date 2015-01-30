using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Utility {
    public class Constants {
        public static List<string> GetRequiredFolders() {
            return Settings.FolderConfig.GetString("requiredFolders").Split(';').ToList();
            
        }
    }
}
