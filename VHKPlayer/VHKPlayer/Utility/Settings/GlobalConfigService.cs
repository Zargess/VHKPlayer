using System;
using System.ComponentModel;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Utility.Settings
{
    public class GlobalConfigService : IGlobalConfigService
    {
        private readonly Setting _setting = new Setting();

        public event PropertyChangedEventHandler FolderSettingsUpdated;
        public event PropertyChangedEventHandler ApplicationSettingsUpdated;
        public event PropertyChangedEventHandler SettingsUpdated;

        public object GetObject(string settingName)
        {
            if (String.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("Setting name must be provided");

            return _setting[settingName];
        }

        public string GetString(string settingName)
        {
            var setting = GetObject(settingName) as string;
            if (setting == null) throw new InvalidOperationException(settingName + " is not a string setting");
            return setting;
        }

        public void Update(string settingName, object value)
        {
            if (string.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("Setting name must be provided");

            var setting = this._setting[settingName];

            if (setting == null)
            {
                throw new ArgumentException("Setting " + this._setting + " not found.");
            }

            if (setting.GetType() != value.GetType())
            {
                throw new InvalidCastException("Unable to cast value to " + setting.GetType());
            }

            _setting[settingName] = value;
            _setting.Save();

            if (settingName.StartsWith("folder") && FolderSettingsUpdated != null)
            {
                FolderSettingsUpdated.Invoke(this, new PropertyChangedEventArgs(settingName));
            }
            else if (settingName.StartsWith("application") && ApplicationSettingsUpdated != null)
            {
                ApplicationSettingsUpdated.Invoke(this, new PropertyChangedEventArgs(settingName));
            }

            SettingsUpdated?.Invoke(this, new PropertyChangedEventArgs(settingName));
        }
    }
}