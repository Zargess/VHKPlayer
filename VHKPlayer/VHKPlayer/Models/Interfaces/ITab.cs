using System.Collections.ObjectModel;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Models.Interfaces
{
    public interface ITab : IDataObserver
    {
        int Number { get; set; }
        string Name { get; set; }
        ObservableCollection<IPlayable> Data { get; set; }
        TabPlacement Placement { get; set; }
        IPlayStrategy PlayStrategy { get; set; }
        bool PlayListTab { get; set; }
        IScript Script { get; set; }
    }
}