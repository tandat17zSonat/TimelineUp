
using System.Collections.Generic;

public class WarriorCollectorData
{
    public int NumberMaxWarrior;
    public List<int> DamageToUpgrade; // Để nâng cấp level
    public List<int> ExpToUpgradeNumberWarrior; // Để nâng cấp số người

    public WarriorCollectorData()
    {
        DamageToUpgrade = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            DamageToUpgrade.Add((i + 1) * 20);
        }

        NumberMaxWarrior = 4;
        ExpToUpgradeNumberWarrior = new List<int>();
        for (int i = 0; i <  NumberMaxWarrior + 1; i++)
        {
            ExpToUpgradeNumberWarrior.Add(i * 100);
        }
    }
}