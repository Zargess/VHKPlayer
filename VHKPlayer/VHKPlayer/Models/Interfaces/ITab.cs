using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models.Interfaces
{
    public interface ITab
    {
        string Name { get; set; }
        IScript Script { get; set; }
        IPlayStrategy PlayStrategy { get; set; }
        bool PlayListTab { get; set; }
    }
}
