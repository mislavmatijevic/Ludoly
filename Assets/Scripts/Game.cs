using System.Collections.Generic;
using UnityEngine;

public enum PlayerCreed
{
    Red,
    Blue,
    Yellow,
    Green,
}

public class Game : MonoBehaviour
{
    private readonly int pointsNeeded = 4;
    private readonly Dictionary<PlayerCreed, int> pointsAchieved;

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

    public void HandlePointAchieved(PlayerCreed creed)
    {
        pointsAchieved[creed]++;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
