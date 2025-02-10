using System.Collections.Generic;
using UnityEngine;

public class GameplayData
{
    public List<WarriorData> ListWarriorDatas;
    public WarriorCollectorConfig ListWarriorCollectorDatas;
    public List<EndBlockData> ListEndBlockDatas;
    public int CountEndBlock;
    public GameplayData()
    {
        // config của warrior----------
        ListWarriorDatas = new List<WarriorData>();
        for (int i = 0; i < 4; i++)
        {
            ListWarriorDatas.Add(new WarriorData()
            {
                Type = 0,
                Level = i,
                Speed = 5,
                ProjectileData = new ProjectileData()
                {
                    Damage = (i + 1) * 10,
                    Speed = 30,
                    Range = (i + 1) * 25 * 1.5f,
                }
            });
        }

        // config của gate_spawn
        ListWarriorCollectorDatas = new WarriorCollectorConfig();

        // config của các khối lúc kết thúc
        CountEndBlock = 3;

        ListEndBlockDatas = new List<EndBlockData>();
        for (int i = 0; i < 10; i++)
        {
            ListEndBlockDatas.Add(new EndBlockData()
            {
                Order = i,
                Hp = (i + 1) * 10,
                Coin = (i + 1) * 5
            });
        }
    }

    public int GetNumberWarriorInCollector()
    {
        return ListWarriorCollectorDatas.ExpToUpgradeNumberWarrior.Count;
    }

    public int GetExpToUpgradeWarriorNumber(int currentLevel)
    {
        return ListWarriorCollectorDatas.ExpToUpgradeNumberWarrior[currentLevel];
    }

    public WarriorData GetWarriorData(int level)
    {
        if (level >= ListWarriorDatas.Count)
        {
            Debug.Log($"Khong co WarriorData level {level}");
            return ListWarriorDatas[0];
        }

        return ListWarriorDatas[level];
    }

    public int GetDamageToUpgradeCollector(int level)
    {
        return ListWarriorCollectorDatas.DamageToUpgrade[level];
    }

    public int GetForwardMoveSpeed()
    {
        return ListWarriorDatas[0].Speed;
    }

    public int GetEndBlockHp(int order)
    {
        return ListEndBlockDatas[order].Hp;
    }
}

public class WarriorCollectorConfig
{
    public List<int> DamageToUpgrade; // Để nâng cấp level
    public List<int> ExpToUpgradeNumberWarrior; // Để nâng cấp số người

    public WarriorCollectorConfig()
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

public class EndBlockData
{
    public int Order;
    public int Hp;
    public int Coin;
}
