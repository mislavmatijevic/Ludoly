using Assets.Scripts.Board.Fields;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PawnSpawn : MonoBehaviour
{
    public StartingField SpawnpointField;

    public HashSet<Pawn> Pawns { get; private set; }
    public PlayerCreed Creed { get; set; }

    private void Start()
    {
        Pawns = GetComponentsInChildren<Pawn>().ToHashSet();
        foreach (Pawn pawn in Pawns)
        {
            pawn.Color = SpawnpointField.GetComponent<Renderer>().material.color;
        }
        Creed = SpawnpointField.Ownership;
    }
}
