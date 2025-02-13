using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayData
{
    public List<WarriorData> ListWarriorDatas; // config của warrior

    public WarriorCollectorData ListWarriorCollectorDatas; // config của collector

    public List<EndBlockData> ListEndBlockDatas;// config của endblock
    public int CountEndBlock;

    public List<List<int>> ListBoosterDatas;

    public List<int> ListBoosterCapacity;

    public List<int> ListMaxBoosterLevel;


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
                Damage = 10 * (i + 1)
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

        // config giá các booster
        ListBoosterDatas = new List<List<int>>();
        for (int i = 0; i < 4; i++)
        {
            var listCost = new List<int>();
            for( int j = 0; j<30; j++)
            {
                listCost.Add((j + 1) * 5);
            }
            ListBoosterDatas.Add(listCost);
        }

        // config lượng kinh nghiệm nhận thêm khi booster capacity
        ListBoosterCapacity = new List<int>();
        for(int i = 0; i< 30; i++)
        {
            ListBoosterCapacity.Add((i + 1) * 5); 
        }

        // booster's max level
        ListMaxBoosterLevel = new List<int>() { 30, 10, 5, 30 };
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


