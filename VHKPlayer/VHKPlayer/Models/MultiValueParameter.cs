using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models
{
    public class MultiValueParameter : IMultiValueParameter
    {
        public IPlayable Playable { get; set; }
        public IPlayStrategy Strategy { get; set; }
    }
}
