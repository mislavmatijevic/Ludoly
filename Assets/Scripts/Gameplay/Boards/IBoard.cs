using System.Collections.Generic;

namespace Assets.Scripts.Board
{
    public interface IBoard
    {
        List<IField> GetFields();
        HashSet<Pawn> GetPawns(PlayerCreed creed);
    }
}
