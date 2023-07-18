using Assets.Scripts.Board;
using Assets.Scripts.Gameplay;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    public GameObject activeBoard;
    public PlayerCreed player1 = PlayerCreed.Red;
    public PlayerCreed player2 = PlayerCreed.Yellow;
    public PlayerCreed player3 = PlayerCreed.None;
    public PlayerCreed player4 = PlayerCreed.None;

    /// <summary>
    /// Sets up the main Game singleton object and starts the game.
    /// </summary>
    void Start()
    {
        Game.Instance.SetBoard(activeBoard.GetComponent<IBoard>());

        var playerHandler = new ActivePlayersHandler
            .ActivePlayersHandlerBuilder()
            .SetPlayer1(player1)
            .SetPlayer2(player2)
            .SetPlayer3(player3)
            .SetPlayer4(player4)
            .Build();

        Game.Instance.SetPlayerHandler(playerHandler);

        Game.Instance.StartGame();
    }
}
