using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Exceptions
{
    public class NoSuchTabException : Exception
    {
        public NoSuchTabException(string message) : base(message)
        {
        }
    }
}
