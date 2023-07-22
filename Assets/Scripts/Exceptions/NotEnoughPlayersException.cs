using System;

namespace Assets.Scripts.Exceptions
{
    internal class InvalidFieldException : ApplicationException
    {
        public InvalidFieldException() : base($"Passed object is not of type {nameof(IField)}! Pawn can only move to {nameof(IField)} position - it's not a bird, for Christ sake!") { }
    }
}
