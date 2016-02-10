using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Utility.FileDelayStrategy.Interfaces
{
    public interface IFileDelayStrategy
    {
        void StartTimer();
        void StopTimer();
    }
}
