using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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
            GetComponent<Light>().enabled = value;
        }
    }

    public Color Color
    {
        set
        {
            GetComponent<Renderer>().material.color = value;
            GetComponent<Light>().color = value;
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
