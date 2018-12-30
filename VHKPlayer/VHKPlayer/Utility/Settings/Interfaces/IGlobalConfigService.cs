using System.ComponentModel;

namespace VHKPlayer.Utility.Settings.Interfaces
{
    public interface IGlobalConfigService
    {
        object GetObject(string settingName);
        string GetString(string settingName);
        void Update(string settingName, object value);

        event PropertyChangedEventHandler FolderSettingsUpdated;
        event PropertyChangedEventHandler ApplicationSettingsUpdated;
        event PropertyChangedEventHandler SettingsUpdated;
    }
}