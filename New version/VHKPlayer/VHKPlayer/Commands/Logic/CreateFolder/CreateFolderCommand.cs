using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateFolder
{
    public class CreateFolderCommand : ICommand
    {
        public string Path { get; set; }
    }
}
