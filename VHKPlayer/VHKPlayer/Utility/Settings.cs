using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Utility {
    public class Settings {
        private static IGlobalConfigService _folderConfig;
        public static IGlobalConfigService FolderConfig {
            get {
                return _folderConfig;
            }
            set {
                if (_folderConfig != null) return;
                _folderConfig = value;
            }
        }
    }
}
