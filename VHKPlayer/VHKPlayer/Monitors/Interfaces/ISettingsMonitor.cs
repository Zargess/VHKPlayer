using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Monitors.Interfaces
{
    public interface ISettingsMonitor
    {
        void AddObserver(ISettingsObserver observer);
        void RemoveObserver(ISettingsObserver observer);
    }
}