using System;

namespace Assets.Scripts.Exceptions
{
    public class NotEnoughPlayersException : ApplicationException
    {
        public NotEnoughPlayersException() : base("No players selected!") { }
    }
}
