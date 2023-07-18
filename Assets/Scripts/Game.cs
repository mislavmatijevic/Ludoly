using Assets.Scripts.Board;
using Assets.Scripts.Gameplay;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public enum PlayerCreed
{
    Red,
    Blue,
    Yellow,
    Green,
    None,
}

public class Game
{
    private IBoard board;

    public static Game Instance { get; } = new();

    private Game() { }

    private int pointsNeeded = 4;
    private readonly Dictionary<Player, int> pointsAchieved;

    private ActivePlayersHandler playerHandler;

    private List<IField> fields;

    public int FieldCount => fields.Count;
    public Player CurrentPlayer { get; private set; }

    public delegate void HandleVictory(Player player);
    public event HandleVictory OnVictory;

    public void SetBoard(IBoard board)
    {
        this.board = board;
        fields = board.GetFields();
    }

    public void SetPlayerHandler(ActivePlayersHandler playerHandler)
    {
        this.playerHandler = playerHandler;
    }

    public void SetMaxPoints(int maxPoints)
    {
        pointsNeeded = maxPoints;
    }

    public void HandlePointAchieved(PlayerCreed creed)
    {
        var player = playerHandler.GetPlayerByCreed(creed);
        CheckVictoryCondition(player);
    }

    private void CheckVictoryCondition(Player player)
    {
        if (player.AchievedPoints == pointsNeeded)
        {
            playerHandler.IsEnabled = false;
            OnVictory?.Invoke(player);
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
        foreach (var currentPlayer in playerHandler.GetNextPlayer())
        {
            CurrentPlayer = currentPlayer;
            break;
        }
    }
}
