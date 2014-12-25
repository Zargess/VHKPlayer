using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.EventHandlers;
using Zargess.VHKPlayer.Model;

namespace Zargess.VHKPlayer.Interfaces {
    public interface IPlayer : IPlayable {
        bool Trainer { get; }
        int Number { get; }
        Statistics Stats { get; }
        List<IPlayerObserver> Observers { get; }
        void AddObserver(IPlayerObserver observer);
        void RemoveObserver(IPlayerObserver observer);
    }
}
