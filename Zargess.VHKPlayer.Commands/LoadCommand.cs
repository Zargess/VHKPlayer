using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Commands {
    public class LoadCommand : IExecuteAble{

        public string Name {
            get { return "load"; }
        }

        public int RequiredArgs {
            get { return 0; }
        }

        public object Run(string[] args) {
            var res = new List
        }
    }
}
