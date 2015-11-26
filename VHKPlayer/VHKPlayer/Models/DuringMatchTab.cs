using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models
{
    public class DuringMatchTab : ITab
    {
        public string Name { get; set; }
        public ObservableCollection<IPlayable> Data { get; set; }

        public bool PlayListTab { get; set; }

        public IPlayStrategy PlayStrategy { get; set; }

        
    }
}
