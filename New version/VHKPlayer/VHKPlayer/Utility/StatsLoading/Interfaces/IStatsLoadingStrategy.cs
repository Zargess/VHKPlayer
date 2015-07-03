using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;

namespace VHKPlayer.Utility.StatsLoading.Interfaces
{
    public interface IStatsLoadingStrategy
    {
        Statistics LoadStats(int number);
    }
}
