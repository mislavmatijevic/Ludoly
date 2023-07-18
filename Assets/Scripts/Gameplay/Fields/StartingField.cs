namespace Assets.Scripts.Board.Fields
{
    internal class StartingField : NormalField
    {
        public PlayerCreed ownership;

        public override void HandleArrival(Pawn pawn)
        {
            if (pawn.creed == ownership)
            {
                Game.Instance.HandlePointAchieved(pawn.creed);
            }

            base.HandleArrival(pawn);
        }
    }
}
