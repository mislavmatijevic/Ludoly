using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public PlayerCreed creed;
    public PawnSpawn spawnPoint;

    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {

    }

    public void Die()
    {
        transform.position = startingPosition;
    }
}
