using UnityEngine;

namespace Assets.Scripts.Board.Fields
{
    internal class NormalField : MonoBehaviour, IField
    {
        public void HandleArrival(Pawn pawn)
        {
            pawn.transform.position = transform.position;
        }
    }
}
