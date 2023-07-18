using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Exceptions
{
    internal class InvalidPlayerException : ApplicationException
    {
        public InvalidPlayerException(string message) : base(message) { }
    }
}
