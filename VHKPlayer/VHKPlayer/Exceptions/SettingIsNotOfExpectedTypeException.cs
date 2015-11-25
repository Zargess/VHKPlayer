using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHKPlayer.Exceptions
{
    public class SettingIsNotOfExpectedTypeException : Exception
    {
        public SettingIsNotOfExpectedTypeException(string message) : base(message)
        {
        }
    }
}
