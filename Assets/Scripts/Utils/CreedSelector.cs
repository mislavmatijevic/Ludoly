using Assets.Scripts.Exceptions;
using System;

namespace Assets.Scripts.Utils
{
    public static class CreedSelector
    {
        public static int GetIndexBasedOnCreed(PlayerCreed creed)
        {
            return creed switch
            {
                PlayerCreed.Red => 0,
                PlayerCreed.Blue => 1,
                PlayerCreed.Yellow => 2,
                PlayerCreed.Green => 3,
                _ => -1,
            };
        }

        internal static PlayerCreed GetCreedBasedOnIndex(int iterator)
        {
            return iterator switch
            {
                0 => PlayerCreed.Red,
                1 => PlayerCreed.Blue,
                2 => PlayerCreed.Yellow,
                3 => PlayerCreed.Green,
                _ => throw new InvalidCreedException($"There is no creed that maps to index {iterator}!"),
            };
        }
    }
}
