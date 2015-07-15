using System.Collections.Generic;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;
using VHKPlayer.Utility.PlayStrategy.Interfaces;

namespace VHKPlayer.Commands.Logic.CreatePlayList
{
    public class CreatePlayListCommand : ICommand
    {
        public FolderNode Folder { get; set; }
        public string Name { get; set; }
        public bool HasAudio { get; set; }
        public ILoadingStrategy<ICollection<FileNode>> LoadingStrategy { get; set; }
        public IPlayStrategy PlayStrategy { get; set; }
    }
}
