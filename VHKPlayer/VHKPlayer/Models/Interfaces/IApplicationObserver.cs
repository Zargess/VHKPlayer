namespace VHKPlayer.Models.Interfaces
{
    public interface IApplicationObserver
    {
        void ApplicationChanged(string settingName);
    }
}