using System;

namespace Assets.Scripts.Exceptions
{
    internal class NotEnoughPlayersException : ApplicationException
    {
        public NotEnoughPlayersException() : base("No players selected!") { }
    }
}
