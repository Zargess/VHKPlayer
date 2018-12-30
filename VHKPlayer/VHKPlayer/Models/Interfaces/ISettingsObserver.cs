namespace VHKPlayer.Models.Interfaces
{
    public interface ISettingsObserver
    {
        void SettingsChanged(string settingName);
    }
}