using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interfaces {
    public interface IStatistics {
        int Goals { get; }
        int Shots { get; }
        int Saves { get; }
        int SaveAttempts { get; }
        int YellowCard { get; }
        int Suspension { get; }
        int RedCard { get; }

        IStatistics Clone();
    }
}