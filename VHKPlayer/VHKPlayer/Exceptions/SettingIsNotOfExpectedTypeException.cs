using System;

namespace VHKPlayer.Exceptions
{
    public class SettingIsNotOfExpectedTypeException : Exception
    {
        public SettingIsNotOfExpectedTypeException(string message) : base(message)
        {
        }
    }
}