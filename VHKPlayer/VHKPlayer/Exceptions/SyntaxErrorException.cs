using System;

namespace VHKPlayer.Exceptions
{
    public class SyntaxErrorException : Exception
    {
        public SyntaxErrorException(string message) : base(message)
        {
        }
    }
}