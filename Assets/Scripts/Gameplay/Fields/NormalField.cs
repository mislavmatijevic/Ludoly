using UnityEngine;

namespace Assets.Scripts.Board.Fields
{
    internal class NormalField : MonoBehaviour, IField
    {
        public virtual void HandleArrival(Pawn pawn)
        {
            pawn.transform.position = transform.position;
            pawn.MovesMade++;
        }
    }
}
