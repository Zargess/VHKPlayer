using System;

namespace VHKPlayer.Exceptions
{
    public class NoSuchTabException : Exception
    {
        public NoSuchTabException(string message) : base(message)
        {
        }
    }
}