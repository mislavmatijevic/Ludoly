using System;

namespace Assets.Scripts.Exceptions
{
    public class InvalidPlayerException : ApplicationException
    {
        public InvalidPlayerException(string message) : base(message) { }
    }
}
