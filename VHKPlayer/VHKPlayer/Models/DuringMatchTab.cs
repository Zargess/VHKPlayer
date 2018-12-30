using System.Collections.ObjectModel;
using VHKPlayer.Models.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models
{
    public class DuringMatchTab : ITab
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public ObservableCollection<IPlayable> Data { get; set; }
        public TabPlacement Placement { get; set; }

        public bool PlayListTab { get; set; }
        public IScript Script { get; set; }

        public IPlayStrategy PlayStrategy { get; set; }


        public void DataUpdated()
        {
        }
    }
}