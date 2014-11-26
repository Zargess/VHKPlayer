using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zargess.VHKPlayer.FileManagement.DataTypes;

namespace Zargess.VHKPlayer.FileManagement.Interfaces {
    public interface IStatsLoadingStrategy {
        Statistics LoadStats(int number);
    }
}
