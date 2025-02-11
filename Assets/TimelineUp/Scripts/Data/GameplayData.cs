using System.Collections.Generic;
using UnityEngine;

public class GameplayData
{
    public List<WarriorData> ListWarriorDatas;
    public WarriorCollectorData ListWarriorCollectorDatas;
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
                Damage = 10* (i + 1)
            });
        }

        // config của gate_spawn
        ListWarriorCollectorDatas = new WarriorCollectorData();

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

    public int GetEndBlockHp(int order)
    {
        return ListEndBlockDatas[order].Hp;
    }
}


