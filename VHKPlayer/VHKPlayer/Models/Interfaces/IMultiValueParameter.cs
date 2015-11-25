using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models.Interfaces
{
    public interface IMultiValueParameter
    {
        IPlayable Playable { get; set; }
        IPlayStrategy Strategy { get; set; }
    }
}
