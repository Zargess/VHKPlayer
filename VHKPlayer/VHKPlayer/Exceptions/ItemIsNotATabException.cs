using System;

namespace VHKPlayer.Exceptions
{
    public class ItemIsNotATabException : Exception
    {
        public ItemIsNotATabException(string message) : base(message)
        {
        }
    }
}