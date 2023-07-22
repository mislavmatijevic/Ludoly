using System;

namespace Assets.Scripts.Exceptions
{
    public class InvalidCreedException : ApplicationException
    {
        public InvalidCreedException(string message) : base(message) { }
    }
}
