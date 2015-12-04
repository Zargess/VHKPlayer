using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Monitors.Interfaces
{
    public interface IApplicationMonitor
    {
        void AddObserver(IApplicationObserver observer);
        void RemoveObserver(IApplicationObserver observer);
    }
}
