using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.PlayerLanguage {
    public class Interpreter {
        private Dictionary<string, Delegate> Functions { get; set; }

        public Interpreter() {
            InitFunctions();
            RunProgram("(getFiles 'testing')");
        }

        private void InitFunctions() {
            Functions = new Dictionary<string, Delegate> {
                {"getFiles", new Func<string, string[]>(Directory.GetFiles)}
            };
        }

        public void RunProgram(string program) {
            
        }
    }
}
