using System.Collections.Generic;

namespace Assets.Scripts.Board
{
    internal interface IBoard
    {
        HashSet<IField> GetFields();
        List<Pawn> GetPawns(PlayerCreed creed);
    }
}
