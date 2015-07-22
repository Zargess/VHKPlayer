using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Models.Interfaces
{
    public interface IDataCenter
    {
        ObservableCollection<Player> Players { get; }
        ObservableCollection<PlayList> PlayLists { get; }
        ObservableCollection<FolderNode> Folders { get; }
        ObservableCollection<PlayableFile> PlayableFiles { get; }
    }
}
