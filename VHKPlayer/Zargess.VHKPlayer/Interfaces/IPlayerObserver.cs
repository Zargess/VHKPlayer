using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayerObserver {
        void StatsChanged(Statistics stat);
    }
}