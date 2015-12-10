using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Models.Interfaces
{
    public interface ITabContainer
    {
        ICollection<ITab> LeftMain { get; }
        ICollection<ITab> RightMain { get; }
        ICollection<ITab> LeftDuringMatch { get; }
        ICollection<ITab> RightDuringMatch { get; }

        ICollection<ITab> GetCollectionFromPlacement(TabPlacement placement);
    }
}
