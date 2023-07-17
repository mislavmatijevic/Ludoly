using Assets.Scripts.Board;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCreed
{
    None,
    Red,
    Blue,
    Yellow,
    Green,
}

public class Game : MonoBehaviour
{
    public IBoard board;

    public static Game instance;
    private Game()
    {
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
    private List<IField> fields;

    public int FieldCount => fields.Count;

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

    private void Start()
    {
        fields = board.GetFields();
    }
}
