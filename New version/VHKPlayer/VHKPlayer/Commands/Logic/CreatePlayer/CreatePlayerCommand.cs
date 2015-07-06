using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;
using VHKPlayer.Utility.LoadingStrategy.Interfaces;

namespace VHKPlayer.Commands.Logic.CreatePlayer
{
    public class CreatePlayerCommand : ICommand
    {
        public FileNode File { get; set; }
        public FolderNode Folder { get; set; }
        public ILoadingStrategy<Statistics> LoadingStrategy { get; set; }
    }
}
