namespace TimelineUp.Data
{
    public class PlayerData
    {
        public int Level = 1;
        public int TimelineId = 0;
        public int EraId = 0;

        public int Energy = 0;
        public int Coin = 50;
        public int Diamond = 0;

        // Level Booster
        public int[] BoosterLevel = { 1, 1, 1, 1 };


        public int ExpCollector = 0;

        // property
        public int NumberOfWarriors = 1;
        public int LevelOfWarriors = 0;

        public int Speed = 10;

        public float ProjectileRate = 1f;
        public float ProjectileRange = 60f;
    }
}

