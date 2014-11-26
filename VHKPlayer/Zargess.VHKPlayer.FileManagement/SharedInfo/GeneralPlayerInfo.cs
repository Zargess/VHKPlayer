using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.Interfaces;
using Zargess.VHKPlayer.SettingsManager;

namespace Zargess.VHKPlayer.FileManagement.SharedInfo {
    public class GeneralPlayerInfo {
        private static GeneralPlayerInfo _instance;
        public static GeneralPlayerInfo Instance {
            get {
                if (_instance == null) {
                    _instance = new GeneralPlayerInfo();
                }
                return _instance;
            }
        }

        private GeneralPlayerInfo() { }

        private IFolder StatsFolder { get; set; }

        public IFolder GetStatsFolder() {
            var path = SettingsManagement.Instance.GetStringSetting("statsFolder");
            if (StatsFolder == null) StatsFolder = new FolderNode(path);
            return StatsFolder;
        }
    }
}
