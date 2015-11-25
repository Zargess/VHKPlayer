using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Commands.Logic.Interfaces;

namespace VHKPlayer.Commands.Logic.ChangeSetting
{
    public class ChangeSettingCommand : ICommand
    {
        public string SettingName { get; set; }
        public object Value { get; set; }
    }
}
