using UnityEngine;

namespace Assets.Scripts.Board.Fields
{
    internal class NormalField : MonoBehaviour, IField
    {
        public virtual void HandleMovingToOwnPosition(Pawn pawn)
        {
            pawn.MoveToField(gameObject);
        }
    }
}
