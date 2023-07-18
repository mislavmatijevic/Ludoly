using Assets.Scripts.Board;
using Assets.Scripts.Gameplay;
using System.Collections.Generic;

public enum PlayerCreed
{
    None,
    Red,
    Blue,
    Yellow,
    Green,
}

public class Game
{
    private IBoard board;

    public static Game Instance { get; private set; } = new Game();

    private Game()
    {
        fields = board.GetFields();

        pointsAchieved = new Dictionary<PlayerCreed, int>()
        {
            { PlayerCreed.Red, 0 },
            { PlayerCreed.Green, 0 },
            { PlayerCreed.Blue, 0 },
            { PlayerCreed.Yellow, 0 },
        };
    }

    private readonly int pointsNeeded = 4;
    private readonly Dictionary<PlayerCreed, int> pointsAchieved;

    private PlayerCreed victor = PlayerCreed.None;
    private PlayerCreed currentPlayer = PlayerCreed.Red;
    private ActivePlayersHandler playerHandler;

    private readonly List<IField> fields;

    public int FieldCount => fields.Count;

    public void SetBoard(IBoard board)
    {
        this.board = board;
    }

    public void SetPlayerHandler(ActivePlayersHandler playerHandler)
    {
        this.playerHandler = playerHandler;
    }

    public void HandlePointAchieved(PlayerCreed creed)
    {
        pointsAchieved[creed]++;
        CheckVictoryCondition(creed);
    }

    private void CheckVictoryCondition(PlayerCreed creed)
    {
        if (pointsAchieved[creed] == pointsNeeded)
        {
            victor = creed;
        }
    }

    private void MovePawn(Pawn pawn, int amount)
    {
        int lastReachableField = pawn.MovesMade + amount;

        if (lastReachableField >= FieldCount)
        {
            lastReachableField -= FieldCount;
        }

        for (int i = pawn.MovesMade; i != lastReachableField; i++)
        {
            if (i >= FieldCount)
            {
                i = 0;
            }

            fields[i].HandleArrival(pawn);
            if (!pawn.IsAlive)
            {
                break;
            }
        }
    }

    public void StartGame()
    {

    }
}
