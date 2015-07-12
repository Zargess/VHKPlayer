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
        public List<Player> Players { get; private set; }
        public List<PlayList> PlayLists { get; private set; }
        public List<FolderNode> Folders { get; private set; }
        public List<PlayableFile> PlayableFiles { get; set; }

        public DataCenter()
        {
            Players = new List<Player>();
            PlayLists = new List<PlayList>();
            Folders = new List<FolderNode>();
            PlayableFiles = new List<PlayableFile>();
        }
    }
}
