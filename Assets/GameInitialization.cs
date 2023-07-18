using Assets.Scripts.Board;
using Assets.Scripts.Gameplay;
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

    /// <summary>
    /// Sets up the main Game singleton object and starts the game.
    /// </summary>
    void Start()
    {
        Game.Instance.SetBoard(activeBoard.GetComponent<IBoard>());

        var playerHandler = new ActivePlayersHandler
            .ActivePlayersHandlerBuilder()
            .SetPlayer(new Player(player1Creed, player1Name))
            .SetPlayer(new Player(player2Creed, player2Name))
            .SetPlayer(new Player(player3Creed, player3Name))
            .SetPlayer(new Player(player4Creed, player4Name))
            .Build();

        Game.Instance.SetPlayerHandler(playerHandler);

        Game.Instance.StartGame();
    }
}
