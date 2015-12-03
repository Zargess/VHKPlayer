using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace VHKPlayer.Models.Interfaces
{
    public interface IDataCenter
    {
        ObservableCollection<Player> Players { get; }
        ObservableCollection<PlayList> PlayLists { get; }
        ObservableCollection<FolderNode> Folders { get; }
        ObservableCollection<PlayableFile> PlayableFiles { get; }
        bool UncommitedChanges { get; set; }

        void AddObserver(IDataObserver observer);
        void RemoveObserver(IDataObserver observer);
        void Commit();
    }
}
