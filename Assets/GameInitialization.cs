using Assets.Scripts.Board;
using Assets.Scripts.Gameplay;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    public GameObject activeBoard;
    public PlayerCreed player1Creed = PlayerCreed.Red;
    public PlayerCreed player2Creed = PlayerCreed.Yellow;
    public PlayerCreed player3Creed = PlayerCreed.None;
    public PlayerCreed player4Creed = PlayerCreed.None;

    public string player1Name = "";
    public string player2Name = "";
    public string player3Name = "";
    public string player4Name = "";

    public TextMeshProUGUI playerRedNameTMP;
    public TextMeshProUGUI playerBlueNameTMP;
    public TextMeshProUGUI playerYellowNameTMP;
    public TextMeshProUGUI playerGreenNameTMP;

    public TextMeshProUGUI playerRedPointsTMP;
    public TextMeshProUGUI playerBluePointsTMP;
    public TextMeshProUGUI playerYellowPointsTMP;
    public TextMeshProUGUI playerGreenPointsTMP;

    public Dice dice;
    public TextMeshProUGUI gameLogText;

    /// <summary>
    /// Sets up the main Game singleton object and starts the game.
    /// </summary>
    void Start()
    {
        Game.Instance.SetBoard(activeBoard.GetComponent<IBoard>());

        List<Player> playerList = new() { new(player1Creed, player1Name), new(player2Creed, player2Name), new(player3Creed, player3Name), new(player4Creed, player4Name) };

        PlayerUIHandler playerRedUi = new(playerList.Find(player => player.Creed == PlayerCreed.Red), playerRedNameTMP, playerRedPointsTMP);
        PlayerUIHandler playerBlueUi = new(playerList.Find(player => player.Creed == PlayerCreed.Blue), playerBlueNameTMP, playerBluePointsTMP);
        PlayerUIHandler playerYellowUi = new(playerList.Find(player => player.Creed == PlayerCreed.Yellow), playerYellowNameTMP, playerYellowPointsTMP);
        PlayerUIHandler playerGreenUi = new(playerList.Find(player => player.Creed == PlayerCreed.Green), playerGreenNameTMP, playerGreenPointsTMP);

        var playerHandler = new ActivePlayersHandler
            .ActivePlayersHandlerBuilder()
            .SetPlayer(playerList[0])
            .SetPlayer(playerList[1])
            .SetPlayer(playerList[2])
            .SetPlayer(playerList[3])
            .Build();

        Game.Instance.SetPlayerHandler(playerHandler);
        Game.Instance.SetPlayerUIHandlers(new List<PlayerUIHandler>() { playerRedUi, playerBlueUi, playerYellowUi, playerGreenUi });
        Game.Instance.SetLogTextHandler(new GameLogUIHandler(gameLogText));

        Game.Instance.StartGame(dice);
    }
}
