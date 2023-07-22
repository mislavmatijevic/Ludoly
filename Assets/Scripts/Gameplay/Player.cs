using System.Collections.Generic;
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

            void DeselectAllPawnsExcept(Pawn selectedPawn)
            {
                foreach (Pawn nonSelectedPawn in pawns)
                {
                    if (nonSelectedPawn != selectedPawn)
                    {
                        nonSelectedPawn.Selected = false;
                    }
                }
            }

            await Task.Run(() =>
            {
                foreach (Pawn p in pawns)
                {
                    p.Selectable = true;
                    p.OnSelectedStateChanged += delegate
                    {
                        if (p.Selected)
                        {
                            DeselectAllPawnsExcept(p);
                        }
                    };
                    p.OnActivated += delegate
                    {
                        DeselectAllPawnsExcept(p);
                        selectedPawn = p;
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
