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
        public float ProjectileRange = 20f;

        public void NextEra()
        {
            EraId = EraId + 1;
            if (EraId >= DataManager.MAX_ERA)
            {
                TimelineId += 1;
                EraId = 0;
            }

            // Reset các biến khác
            Coin = 0;
            BoosterLevel = new int[] { 1, 1, 1, 1 };
            ExpCollector = 0;
            NumberOfWarriors = 1;
            LevelOfWarriors = 0;
        }

        public bool CheckNextEra()
        {
            if (TimelineId == DataManager.MAX_TIMELINE - 1 && EraId == DataManager.MAX_ERA - 1)
            {
                return false;
            }
            return true;
        }
    }
}

