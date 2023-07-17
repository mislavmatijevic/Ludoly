using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Board.Fields
{
    internal class StartingField : MonoBehaviour, IField
    {
        public PlayerCreed ownership;

        public void HandleArrival(Pawn pawn)
        {
            if (pawn.creed == ownership)
            {
                Game.instance.HandlePointAchieved(pawn.creed);
            }
        }
    }
}
