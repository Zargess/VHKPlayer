using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;
using VHKPlayer.Models;

namespace VHKPlayer.Commands.Logic.CreateFile
{
    public class CreateFileCommand : ICommand
    {
        public string Path { get; set; }
        public FolderNode Folder { get; set; }
    }
}
