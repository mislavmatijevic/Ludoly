using System;

namespace Assets.Scripts.Exceptions
{
    internal class NotEnoughPlayableCreedsException : ApplicationException
    {
        public NotEnoughPlayableCreedsException() : base("No players selected!") { }
    }
}
