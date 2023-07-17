using Assets.Scripts.Board;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Boards
{
    internal class DefaultBoard : MonoBehaviour, IBoard
    {
        private List<IField> fields;
        public PawnSpawn redPawnSpawn;
        public PawnSpawn bluePawnSpawn;
        public PawnSpawn yellowPawnSpawn;
        public PawnSpawn greenPawnSpawn;

        public Dictionary<PlayerCreed, PawnSpawn> pawnSpawns;

        private void Start()
        {
            fields = GetComponentsInChildren<IField>().ToList();
            pawnSpawns = new Dictionary<PlayerCreed, PawnSpawn>()
            {
                {PlayerCreed.Red, redPawnSpawn },
                {PlayerCreed.Blue, bluePawnSpawn },
                {PlayerCreed.Yellow, yellowPawnSpawn },
                {PlayerCreed.Green, greenPawnSpawn }
            };
        }

        public List<IField> GetFields()
        {
            return fields;
        }

        public HashSet<Pawn> GetPawns(PlayerCreed creed)
        {
            return pawnSpawns[creed].Pawns;
        }
    }
}
