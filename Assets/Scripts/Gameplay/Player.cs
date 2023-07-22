using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;

namespace Assets.Scripts.Gameplay
{
    public class Player
    {
        public PlayerCreed Creed { get; set; }
        public string Name { get; set; }
        public int AchievedPoints { get; set; } = 0;

        public delegate void PointAchieved();
        public event PointAchieved OnPointAchieved;

        public Player(PlayerCreed creed, string name)
        {
            Creed = creed;
            Name = name;
        }

        internal Pawn SelectPawn(HashSet<Pawn> pawns)
        {
            return pawns.GetEnumerator().Current;
        }
    }
}
