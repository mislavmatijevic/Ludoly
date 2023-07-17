using UnityEngine;

public class Pawn : MonoBehaviour
{
    public PlayerCreed creed;
    public PawnSpawn spawnPoint;

    public bool IsAlive { get; set; } = true;

    private Vector3 startingPosition;

    public int MovesMade { get; set; } = 0;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {

    }

    public void Die()
    {
        transform.position = startingPosition;
        IsAlive = false;
        MovesMade = 0;
    }
}
