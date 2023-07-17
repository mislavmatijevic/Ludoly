namespace Assets.Scripts.Board.Fields
{
    internal class StartingField : NormalField
    {
        public PlayerCreed ownership;

        public override void HandleArrival(Pawn pawn)
        {
            if (pawn.creed == ownership)
            {
                Game.instance.HandlePointAchieved(pawn.creed);
            }

            base.HandleArrival(pawn);
        }
    }
}
