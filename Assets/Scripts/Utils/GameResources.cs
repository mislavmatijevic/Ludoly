using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils
{
    internal static class GameResources
    {
        private static readonly List<string> str_diceRollingStrs = new() {
            "Our Father, Who art in heaven...",
            "Come on, come on!",
            "Alea iacta est!",
            "I have a good feeling...",
            "To 6 or not to 6, that is a question.",
            "Don't mess it up now, dice!",
            "All or nothing!"
        };

        public static string STR_DiceRolling
        {
            get
            {
                var randomInteger = (int)(UnityEngine.Random.value * 10 * str_diceRollingStrs.Count);
                return str_diceRollingStrs[randomInteger % str_diceRollingStrs.Count];
            }
        }
    }
}
