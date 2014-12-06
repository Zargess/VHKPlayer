using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.Utility {
    public class GeneralFunctions {
        public static int StringToInteger(string s) {
            int n;
            int.TryParse(s, out n);
            return n;
        }

        public static string[] ConstructElements(string s) {
            s = s.Replace("{", "");
            s = s.Replace("}", "");
            var res = s.Split(';');
            return res;
        }
    }
}
