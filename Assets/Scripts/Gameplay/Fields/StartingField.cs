namespace Assets.Scripts.Board.Fields
{
    public class StartingField : NormalField
    {
        public PlayerCreed Ownership;

        public override void HandleMovingToOwnPosition(Pawn pawn)
        {
            if (pawn.Creed == Ownership)
            {
                Game.Instance.HandlePointAchieved(pawn.Creed);
            }

            base.HandleMovingToOwnPosition(pawn);
        }
    }
}
