
using System.Collections.Generic;

public class WarriorCollectorData
{
    public List<int> DamageToUpgrade; // Để nâng cấp level
    public List<int> ExpToUpgradeNumberWarrior; // Để nâng cấp số người

    public WarriorCollectorData()
    {
        DamageToUpgrade = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            DamageToUpgrade.Add((i + 1) * 20);
        }

        ExpToUpgradeNumberWarrior = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            ExpToUpgradeNumberWarrior.Add((i + 1) * 100);
        }
    }
}