using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IStatsLoadingStrategy {
        Statistics LoadStats(int number);
    }
}