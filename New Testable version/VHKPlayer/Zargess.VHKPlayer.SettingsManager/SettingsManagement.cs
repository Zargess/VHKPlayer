using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.SettingsManager {
    public class SettingsManagement {
        public static object GetSetting(string key) {
            return Settings.Default[key];
        }

        public static string GetStringSetting(string key) {
            return Settings.Default[key] as string;
        }

        public static void SetSetting(string key, object value) {
            if (Settings.Default[key] == null) return;
            Settings.Default[key] = value;
            Settings.Default.Save();
        }

        public static void Reset() {
            Settings.Default.Reset();
        }
    }
}
