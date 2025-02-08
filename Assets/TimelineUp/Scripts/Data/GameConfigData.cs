using System.Collections.Generic;
using UnityEngine;

public class GameConfigData
{
    public List<TimelineData> ListTimelines;
    public List<WarriorData> ListWarriorDatas;

    public GameConfigData()
    {
        ListTimelines = new List<TimelineData>();
        for (int i = 0; i < 5; i++)
        {
            ListTimelines.Add(new TimelineData());
        }

        //---------------------------------------------
        ListWarriorDatas = new List<WarriorData>();
        for (int i = 0; i < 4; i++)
        {
            ListWarriorDatas.Add(new WarriorData()
            {
                Type = 0,
                Level = i,
                Speed = 25,
                ProjectileData = new ProjectileData()
                {
                    Damage = i + 1,
                    Speed = 25,
                    Range = (i + 1) * 75 * 1.5f,
                }
            });
        }
    }

    public int GetNumberLevelCollector()
    {
        var timeline = ListTimelines[0];
        var era = timeline.ListEraData[0];
        return era.ExpToUpgradeCollector.Count;
    }

    public int GetExpToUpgrade(int timelineId, int eraId, int currentLevel)
    {
        var timeline = ListTimelines[timelineId];
        var era = timeline.ListEraData[eraId];
        return era.ExpToUpgradeCollector[currentLevel];
    }

    public WarriorData GetWarriorData(int level)
    {
        if( level >= ListWarriorDatas.Count)
        {
            Debug.Log($"Khong co WarriorData level {level}");
            return ListWarriorDatas[0];
        }

        return ListWarriorDatas[level];
    }
}

public class TimelineData
{
    public int Id;
    public List<EraData> ListEraData;

    public TimelineData()
    {
        ListEraData = new List<EraData>();
        for (int i = 0; i < 5; i++)
        {
            ListEraData.Add(new EraData());
        }
    }
}

public class EraData
{
    public List<int> ExpToUpgradeCollector; // exp để collector nâng cấp 0, 1, 2, 3, 4

    public EraData()
    {
        ExpToUpgradeCollector = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            ExpToUpgradeCollector.Add(i * 5);
        }
    }
}
