using Zargess.VHKPlayer.FileManagement.EventHandlers;
using Zargess.VHKPlayer.FileManagement.DataTypes;

namespace Zargess.VHKPlayer.FileManagement.Interfaces {
    public interface IPlayer : IPlayable {
        bool Trainer { get; }
        int Number { get; }
        Statistics Stats { get; }
        event StatsChangedHandler StatsChanged;
    }
}