using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Utility.Settings.Interfaces;

namespace VHKPlayer.Utility.Settings
{
    public class GlobalConfigService : IGlobalConfigService
    {
        private Setting setting = new Setting();

        public event PropertyChangedEventHandler FolderChanged;
        public event PropertyChangedEventHandler PlayerChanged;

        public object GetObject(string settingName)
        {
            if (String.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("Setting name must be provided");

            return setting[settingName];
        }

        public string GetString(string settingName)
        {
            var setting = GetObject(settingName) as string;
            if (setting == null) throw new InvalidOperationException(settingName + " is not a string setting");
            return setting;
        }

        public void Update(string settingName, object value)
        {
            if (String.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("Setting name must be provided");

            var setting = this.setting[settingName];

            if (setting == null)
            {
                throw new ArgumentException("Setting " + this.setting + " not found.");
            }
            else if (setting.GetType() != value.GetType())
            {
                throw new InvalidCastException("Unable to cast value to " + setting.GetType());
            }
            else
            {
                this.setting[settingName] = value;
                this.setting.Save();
                if (settingName.StartsWith("folder") && FolderChanged != null)
                {
                    FolderChanged.Invoke(this, new PropertyChangedEventArgs(settingName));
                } else if (PlayerChanged != null)
                {
                    PlayerChanged.Invoke(this, new PropertyChangedEventArgs(settingName));
                }
            }
        }
    }
}
