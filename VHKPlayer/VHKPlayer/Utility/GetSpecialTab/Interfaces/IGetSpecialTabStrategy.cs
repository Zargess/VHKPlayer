using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Utility.GetSpecialTab.Interfaces
{
    public interface IGetSpecialTabStrategy
    {
        bool IsSpecialTab(string def);
        ITab CreateSpecialTab(string name);
    }
}
