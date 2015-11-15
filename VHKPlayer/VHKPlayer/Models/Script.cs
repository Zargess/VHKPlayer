using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models.Interfaces;

namespace VHKPlayer.Models
{
    public class Script : IScript
    {
        public string Code { get; private set; }
        public Script(string script)
        {
            Code = script;
        }
    }
}
