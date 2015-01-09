using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Interfaces;

namespace VHKPlayer.Utility {
    public class GlobalConfigService : IGlobalConfigService {
        private ISettings _settings;

        public event PropertyChangedEventHandler PropertyChanged;

        public GlobalConfigService(ISettings settings) {
            _settings = settings;
        }

        public object Get(string settingName) {
            if (String.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("Setting name must be provided");

            return _settings[settingName];
        }

        public string GetString(string settingName) {
            var setting = Get(settingName) as string;
            if (setting == null) throw new InvalidOperationException(settingName + " is not a string setting");
            return setting;
        }

        public void Update(string settingName, object value) {
            if (String.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("Setting name must be provided");

            var Setting = _settings[settingName];

            if (Setting == null) {
                throw new ArgumentException("Setting " + _settings + " not found.");
            } else if (Setting.GetType() != value.GetType()) {
                throw new InvalidCastException("Unable to cast value to " + Setting.GetType());
            } else {
                _settings[settingName] = value;
                _settings.Save();
                if (PropertyChanged != null) {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(settingName));
                }
            }
        }
    }
}
