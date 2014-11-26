using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.SettingsManager {
    public class SettingsManagement : ISettingsManager {
        private SettingsManagement() {}

        public object GetSetting(string key) {
            return Settings.Default[key];
        }

        public string GetStringSetting(string key) {
            return Settings.Default[key] as string;
        }

        public void SetSetting(string key, object value) {
            if (Settings.Default[key] == null) return;
            Settings.Default[key] = value;
            Settings.Default.Save();
        }

        public void Reset() {
            Settings.Default.Reset();
        }

        public string GetPathSetting(string key, int index) {
            var setting = GetStringSetting(key).Split(';');
            return setting[index];
        }

        private static SettingsManagement instance;
        public static SettingsManagement Instance {
            get {
                if(instance == null) {
                    instance = new SettingsManagement();
                }
                return instance;
            }
        }
    }
}