using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils
{
    internal static class GameResources
    {
        private static int GetRandomIndex(int max)
        {
            return (int)(UnityEngine.Random.value * 10 * max) % max;
        }

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
                return str_diceRollingStrs[GetRandomIndex(str_diceRollingStrs.Count)];
            }
        }

        /// <summary>
        /// A string "{username}" is replaced by actual player's username.
        /// </summary>
        private static readonly Dictionary<int, List<string>> str_diceResultStrs = new() {
            {1, new() { "Hey, everyone! Go tell {username}'s friends {username} got a 1 and is a terrible person due to that fact!", "LOL, {username} got a 1!", "My oh my. Is that a 1, {username}?", "{username} got a 1 and I got to wonder how does {username} manage day-to-day life with luck this bad?", "In an alternative universe, {username} got a 6! However, {username} is on a wrong timeline and got this pathetic 1 instead.", "My condolences for your 1, {username}.", "They say your luck equals your looks, {username}. Dwell on it.", "Ha-ha! {username} is a 1-getter. Poor thing.", "You know what, {username}? I say you don't even deserve better than a 1.", "{username} got into quarrel with the lady luck and ended up having a 1 on the dice stuck." }},
            {2, new() { "My grandma gets more than 2, {username}!", "You ain't in a hurry, {username}? Or is it just a skill issue? Yep, thought so.", "{username}, you know what they say about 2: 'It's better than 1. (™)'", "{username} is either trying to win with bad numbers such as 2 or just sucks at this game." }},
            {3, new() { "On a scale from 1-12, this is a 6! Just like you're a 10 on a scale 1-20, {username}.", "Try not to kill anyone with 3, {username}. Not like you could.", "I guess {username} really likes an average of 3.", "'3. Not great, not terrible.' - {username}" }},
            {4, new() { "Good players get a 6. {username}'s kind gets a 4. Live with it, {username}.", "Number 4 is boring - just like {username}!", "I say 4 is just enough for someone like {username}.", "It seems {username} is just too scared to go faster than 4 steps at a time." }},
            {5, new() { "Yeah, well, 5 ain't a 6, but it's gonna get {username} somewhere alright.", "Someone tell {username} that dice also rolls above 5!", "I think {username} is like: 'Who needs a 6 when you can win with a 5 anyway.'" }},
            {6, new() { "Behold, for the dice has spoken: {username} is worthy of a 6.", "I knew {username} would get a 6!", "Stand clear folks, {username} is on a rampage with 6!", "{username} is ruining all the fun when he doesn't even break a sweat to get a 6.", "6 - {username}'s on it!", "Give 'em hell with that 6, {username}!", "How about everybody just declares {username} a winner and go play something else? No point in playing against a 6-gainer like {username}." }},
        };
        public static string STR_GetDiceResultMsg(int value, Gameplay.Player currentPlayer)
        {
            var responsesList = str_diceResultStrs[value];
            return responsesList[GetRandomIndex(responsesList.Count)].Replace("{username}", currentPlayer.Name);
        }
    }
}
