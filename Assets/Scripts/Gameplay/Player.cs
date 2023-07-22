﻿using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Pawn> SelectOnePawnAsync(HashSet<Pawn> pawns)
        {
            Pawn selectedPawn = null;
            await Task.Run(() =>
            {
                foreach (Pawn p in pawns)
                {
                    p.Selectable = true;
                    p.OnSelectionStateChanged += delegate
                    {
                        selectedPawn = p;
                        foreach (Pawn nonSelectedPawn in pawns)
                        {
                            if (nonSelectedPawn != selectedPawn)
                            {
                                nonSelectedPawn.Selected = false;
                            }
                        }
                    };
                }
                while (selectedPawn == null)
                {
                    _ = Task.Yield();
                }
            });
            return selectedPawn;
        }
    }
}
