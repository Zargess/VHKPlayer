using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Infrastructure
{
    public static class Extensions
    {
        public static int ToInteger(this string s)
        {
            int n;
            int.TryParse(s, out n);
            return n;
        }
    }
}
