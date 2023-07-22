using Assets.Scripts.Board;
using Assets.Scripts.Gameplay;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    public GameObject ActiveBoard;
    public PlayerCreed Player1Creed = PlayerCreed.Red;
    public PlayerCreed Player2Creed = PlayerCreed.Yellow;
    public PlayerCreed Player3Creed = PlayerCreed.None;
    public PlayerCreed Player4Creed = PlayerCreed.None;

    public string Player1Name = "";
    public string Player2Name = "";
    public string Player3Name = "";
    public string Player4Name = "";

    public CanvasRenderer PlayerRedCanvas;
    public CanvasRenderer PlayerBlueCanvas;
    public CanvasRenderer PlayerYellowCanvas;
    public CanvasRenderer PlayerGreenCanvas;

    public Dice Dice;
    public TextMeshProUGUI GameLogText;

    /// <summary>
    /// Sets up the main Game singleton object and starts the game.
    /// </summary>
    private void Start()
    {
        Game.Instance.SetBoard(ActiveBoard.GetComponent<IBoard>());

        List<Player> PlayerList = new() { new(Player1Creed, Player1Name), new(Player2Creed, Player2Name), new(Player3Creed, Player3Name), new(Player4Creed, Player4Name) };

        PlayerUIHandler PlayerRedUi = new(PlayerList.Find(Player => Player.Creed == PlayerCreed.Red), PlayerRedCanvas);
        PlayerUIHandler PlayerBlueUi = new(PlayerList.Find(Player => Player.Creed == PlayerCreed.Blue), PlayerBlueCanvas);
        PlayerUIHandler PlayerYellowUi = new(PlayerList.Find(Player => Player.Creed == PlayerCreed.Yellow), PlayerYellowCanvas);
        PlayerUIHandler PlayerGreenUi = new(PlayerList.Find(Player => Player.Creed == PlayerCreed.Green), PlayerGreenCanvas);

        ActivePlayersHandler PlayerHandler = new ActivePlayersHandler
            .ActivePlayersHandlerBuilder()
            .SetPlayer(PlayerList[0])
            .SetPlayer(PlayerList[1])
            .SetPlayer(PlayerList[2])
            .SetPlayer(PlayerList[3])
            .Build();

        Game.Instance.SetPlayerHandler(PlayerHandler);
        Game.Instance.SetPlayerUIHandlers(new List<PlayerUIHandler>() { PlayerRedUi, PlayerBlueUi, PlayerYellowUi, PlayerGreenUi });
        Game.Instance.SetLogTextHandler(new GameLogUIHandler(GameLogText));

        Game.Instance.PlayGame(Dice);
    }
}
