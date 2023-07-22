using UnityEngine;

public class Pawn : MonoBehaviour
{
    public PawnSpawn spawnPoint;

    public PlayerCreed Creed { get; private set; }
    public bool IsAlive { get; set; } = true;

    private Vector3 startingPosition;

    public int MovesMade { get; set; } = 0;
    public bool Highlight
    {
        set
        {
            if (value == true)
            {
                
            }
        }
    }

    private void Start()
    {
        startingPosition = transform.position;
        Creed = spawnPoint.creed;
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
