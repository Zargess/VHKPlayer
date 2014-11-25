using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.SettingsManager {
    public interface ISettingsManager {
        object GetSetting(string key);
        string GetStringSetting(string key);
        string GetPathSetting(string key, int index);
        void SetSetting(string key, object value);
        void Reset();
    }
}
