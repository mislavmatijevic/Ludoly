using Assets.Scripts.Board;
using Assets.Scripts.Gameplay;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using System.Collections.Generic;

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
    public static Game Instance { get; } = new();

    private Game() { }

    private int _pointsNeeded = 4;

    private ActivePlayersHandler _playerHandler;

    private IBoard _board;
    private List<IField> _fields;
    private List<PlayerUIHandler> _playerUIHandlers;
    private GameLogUIHandler _gameLogUIHandler;

    public int FieldCount => _fields.Count;
    public Player CurrentPlayer { get; private set; }

    public delegate void HandleVictory(Player player);
    public event HandleVictory OnVictory;

    public delegate void MessageEvent(string message);
    public event MessageEvent OnMessageEvent;

    public void SetBoard(IBoard board)
    {
        _board = board;
        _fields = board.GetFields();
    }

    public void SetPlayerHandler(ActivePlayersHandler playerHandler)
    {
        _playerHandler = playerHandler;
    }

    public void SetMaxPoints(int maxPoints)
    {
        _pointsNeeded = maxPoints;
    }

    public void SetPlayerUIHandlers(List<PlayerUIHandler> playerUIHandlers)
    {
        _playerUIHandlers = playerUIHandlers;
    }

    public void SetLogTextHandler(GameLogUIHandler gameLogUIHandler)
    {
        _gameLogUIHandler = gameLogUIHandler;
    }

    public void HandlePointAchieved(PlayerCreed creed)
    {
        Player player = _playerHandler.GetPlayerByCreed(creed);
        player.AchievedPoints += 1;
        CheckVictoryCondition(player);
    }

    private void CheckVictoryCondition(Player player)
    {
        if (player.AchievedPoints == _pointsNeeded)
        {
            DeclareVictory(player);
        }
    }

    private void DeclareVictory(Player player)
    {
        _playerHandler.IsEnabled = false;
        OnVictory?.Invoke(player);

        foreach (PlayerUIHandler uiHandler in _playerUIHandlers)
        {
            uiHandler.Dispose();
        }

        _playerUIHandlers = null;
    }

    private void MovePawn(Pawn pawn, int moveAmount)
    {
        if (!pawn.IsOnTheBoard)
        {
            pawn.SpawnOnSpawnpoint();
            moveAmount--;
        }

        int currentFieldIndex = _fields.FindIndex(field => field == pawn.CurrentField);
        int lastReachableField = currentFieldIndex + moveAmount;

        if (lastReachableField >= FieldCount)
        {
            lastReachableField -= FieldCount;
        }

        do
        {
            currentFieldIndex++;

            if (currentFieldIndex >= FieldCount)
            {
                currentFieldIndex = 0;
            }

            _fields[currentFieldIndex].HandleMovingToOwnPosition(pawn);

        } while (currentFieldIndex != lastReachableField);
    }

    public async void PlayGame(Dice dice)
    {
        foreach (Player currentPlayer in _playerHandler.GetNextPlayer())
        {
            CurrentPlayer = currentPlayer;

            HashSet<Pawn> pawns = _board.GetPawns(CurrentPlayer.Creed);
            ChangePawnsHighligh(pawns, true);

            OnMessageEvent?.Invoke(GameResources.STR_DiceRolling);

            int diceResult = await dice.RollDice();
            OnMessageEvent?.Invoke(GameResources.STR_GetDiceResultMsg(diceResult, CurrentPlayer));

            Pawn selectedPawn = await CurrentPlayer.SelectOnePawnAsync(pawns);
            MovePawn(selectedPawn, diceResult);

            ChangePawnsHighligh(pawns, false);
        }
    }

    private void ChangePawnsHighligh(HashSet<Pawn> pawns, bool doHighlight)
    {
        foreach (Pawn pawn in pawns)
        {
            pawn.Highlight = doHighlight;
        }
    }
}
