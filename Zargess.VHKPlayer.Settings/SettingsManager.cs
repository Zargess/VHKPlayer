using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Settings {
    public class SettingsManager {
        public static object GetSetting(string key) {
            return Settings.Default[key];
        }

        public static void SetSetting(string key, object value) {
            if (Settings.Default[key] == null) return;
            Settings.Default[key] = value;
            Settings.Default.Save();
        }
    }
}
