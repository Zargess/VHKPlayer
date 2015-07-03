using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Models
{
    public class DataCenter
    {
        public ObservableCollection<Player> Players { get; private set; }

        public DataCenter()
        {
            Players = new ObservableCollection<Player>();
        }

        public void AddPlayer(Player p)
        {
            Players.Add(p);
        }
    }
}
