using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Monitors.Interfaces
{
    public interface ISettingsMonitor
    {
        void AddObserver(ISettingsObserver observer);
        void RemoveObserver(ISettingsObserver observer);
    }
}
