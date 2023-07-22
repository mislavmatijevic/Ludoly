using Assets.Scripts.Board;
using Assets.Scripts.Gameplay;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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
    private List<PlayerUIHandler> playerUIHandlers;
    private GameLogUIHandler gameLogUIHandler;

    public int FieldCount => fields.Count;
    public Player CurrentPlayer { get; private set; }

    public delegate void HandleVictory(Player player);
    public event HandleVictory OnVictory;

    public delegate void MessageEvent(string message);
    public event MessageEvent OnMessageEvent;

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

    public void SetPlayerUIHandlers(List<PlayerUIHandler> playerUIHandlers)
    {
        this.playerUIHandlers = playerUIHandlers;
    }

    public void SetLogTextHandler(GameLogUIHandler gameLogUIHandler)
    {
        this.gameLogUIHandler = gameLogUIHandler;
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
            DeclareVictory(player);
        }
    }

    private void DeclareVictory(Player player)
    {
        playerHandler.IsEnabled = false;
        OnVictory?.Invoke(player);

        foreach (var uiHandler in playerUIHandlers)
        {
            uiHandler.Dispose();
        }

        playerUIHandlers = null;
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

            fields[i].HandleMovingToOwnPosition(pawn);
            if (!pawn.IsAlive)
            {
                break;
            }
        }
    }

    public async void PlayGame(Dice dice)
    {
        foreach (var currentPlayer in playerHandler.GetNextPlayer())
        {
            OnMessageEvent?.Invoke(GameResources.STR_DiceRolling);
            int diceResult = await dice.RollDice();
            CurrentPlayer = currentPlayer;
            OnMessageEvent?.Invoke(GameResources.STR_GetDiceResultMsg(diceResult, CurrentPlayer));
            Pawn selectedPawn = await GetSelectedPawnAsync(CurrentPlayer);
            MovePawn(selectedPawn, diceResult);
        }
    }

    private async Task<Pawn> GetSelectedPawnAsync(Player player)
    {
        HashSet<Pawn> pawns = board.GetPawns(player.Creed);
        foreach (var pawn in pawns)
        {
            pawn.Highlight = true;
        }
        return await player.SelectOnePawnAsync(board.GetPawns(CurrentPlayer.Creed));
    }
}
