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

        public int NumberInCollector = 1;
        public int ExpCollector = 0;

        // property
        public int NumberOfWarriors = 1;
        public int LevelOfWarriors = 0;

        public float Speed = 4f;

        public float ProjectileRate = 2f;
        public float ProjectileRange = 12f;

        public void NextEra()
        {
            EraId = EraId + 1;
            if (EraId >= DataManager.MAX_ERA_NUMBER)
            {
                TimelineId += 1;
                EraId = 0;
            }

            // Reset các biến khác
            Coin = 0;
            BoosterLevel = new int[] { 1, 1, 1, 1 };

            NumberInCollector = 1;
            ExpCollector = 0;
            NumberOfWarriors = 1;
            LevelOfWarriors = 0;
        }

        public bool CheckNextEra()
        {
            if (TimelineId == DataManager.MAX_TIMELINE_NUMBER - 1 && EraId == DataManager.MAX_ERA_NUMBER - 1)
            {
                return false;
            }
            return true;
        }
    }
}

