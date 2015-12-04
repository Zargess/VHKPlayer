using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Models.Interfaces
{
    public interface ITabContainer
    {
        ICollection<ITab> Tabs { get; } 
    }
}
