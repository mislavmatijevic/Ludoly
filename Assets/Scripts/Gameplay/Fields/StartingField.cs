namespace Assets.Scripts.Board.Fields
{
    internal class StartingField : NormalField
    {
        public PlayerCreed ownership;

        public override void HandleMovingToOwnPosition(Pawn pawn)
        {
            if (pawn.Creed == ownership)
            {
                Game.Instance.HandlePointAchieved(pawn.Creed);
            }

            base.HandleMovingToOwnPosition(pawn);
        }
    }
}
