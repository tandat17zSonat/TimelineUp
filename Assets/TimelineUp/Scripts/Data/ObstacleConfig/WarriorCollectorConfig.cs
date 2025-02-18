using System.Collections.Generic;

namespace TimelineUp.Data
{
    [System.Serializable]
    public class WarriorCollectorConfig
    {
        // level và số warrior nhận được
        public List<int> DamageToUpgradeLevel; // Để nâng cấp level: Damage cần nhận để qua level này
        public List<int> ExpToUpgradeNumberWarrior; // i = số người, Để nâng cấp số người

        public int GetMaxWarriorNumber()
        {
            return ExpToUpgradeNumberWarrior.Count;
        }

        public int GetMaxWarriorLevel()
        {
            return DamageToUpgradeLevel.Count - 1;
        }
        //public WarriorCollectorConfig()
        //{
        //    DamageToUpgradeLevel = new List<int>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        DamageToUpgradeLevel.Add(i * 10); // qua level 1 cần 10 damage
        //    }

        //    ExpToUpgradeNumberWarrior = new List<int>();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        ExpToUpgradeNumberWarrior.Add(i * 36); // qua level i cần 36 * i exp
        //    }
        //}

    }
}
