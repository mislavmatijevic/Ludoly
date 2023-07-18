namespace Assets.Scripts.Gameplay
{
    public class Player
    {
        public PlayerCreed Creed { get; set; }
        public string Name { get; set; }
        public int AchievedPoints { get; set; } = 0;

        public Player(PlayerCreed creed, string name)
        {
            Creed = creed;
            Name = name;
        }
    }
}
