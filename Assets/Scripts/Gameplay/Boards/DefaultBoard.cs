using Assets.Scripts.Board;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Boards
{
    public class DefaultBoard : MonoBehaviour, IBoard
    {
        public PawnSpawn RedPawnSpawn;
        public PawnSpawn BluePawnSpawn;
        public PawnSpawn YellowPawnSpawn;
        public PawnSpawn GreenPawnSpawn;

        private List<IField> _fields;
        public Dictionary<PlayerCreed, PawnSpawn> PawnSpawns { get; set; }

        private void Start()
        {
            _fields = GetComponentsInChildren<IField>().ToList();
            PawnSpawns = new Dictionary<PlayerCreed, PawnSpawn>()
            {
                {PlayerCreed.Red, RedPawnSpawn },
                {PlayerCreed.Blue, BluePawnSpawn },
                {PlayerCreed.Yellow, YellowPawnSpawn },
                {PlayerCreed.Green, GreenPawnSpawn }
            };
        }

        public List<IField> GetFields()
        {
            return _fields;
        }

        public HashSet<Pawn> GetPawns(PlayerCreed creed)
        {
            return PawnSpawns[creed].Pawns;
        }
    }
}
