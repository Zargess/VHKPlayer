using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interfaces {
    public interface IPlayer : IPlayable {
        bool Trainer { get; }
        int Number { get; }
        IStatistics Stats { get; }

        void AddObserver(IPlayerObserver observer);
        void RemoveObserver(IPlayerObserver observer);
    }
}