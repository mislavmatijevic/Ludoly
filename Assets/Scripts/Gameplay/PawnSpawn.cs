using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PawnSpawn : MonoBehaviour
{
    public PlayerCreed creed;
    public GameObject spawnpointField;

    public HashSet<Pawn> Pawns { get; private set; }

    private void Start()
    {
        Pawns = GetComponentsInChildren<Pawn>().ToHashSet();
    }

    private void Update()
    {

    }
}
