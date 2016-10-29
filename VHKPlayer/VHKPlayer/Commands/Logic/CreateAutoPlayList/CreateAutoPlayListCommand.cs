using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.CreatePlayList;
using VHKPlayer.Commands.Logic.Interfaces;

namespace VHKPlayer.Commands.Logic.CreateAutoPlayList
{
    public class CreateAutoPlayListCommand : ICommand
    {
        public CreatePlayListCommand Command { get; set; }
    }
}
