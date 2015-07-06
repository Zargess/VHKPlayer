using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Commands.Logic.CreatePlayList
{
    public class CreatePlayListCommand : ICommand
    {
        // TODO : Add a PlayStrategy here to so that playlists knows how they should be played
        public FolderNode Folder { get; set; }
        public string Name { get; set; }
        public bool HasAudio { get; set; }
        public ILoadingStrategy<ICollection<FileNode>> LoadingStrategy { get; set; }
        public IPlayStrategy PlayStrategy { get; set; }
    }
}
