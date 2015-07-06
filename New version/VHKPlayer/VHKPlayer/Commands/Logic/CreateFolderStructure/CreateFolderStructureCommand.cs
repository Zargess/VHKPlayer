using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateFolderStructure
{
    public class CreateFolderStructureCommand : ICommand
    {
        public string RootFolderPath { get; set; }
    }
}
