using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Interfaces {
    public interface IStatsLoadingStrategy {
        IStatistics LoadStats(int number);
    }
}
