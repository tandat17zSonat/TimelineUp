using System.Collections.Generic;

public class GameConfigData
{
    private List<TimelineData> ListTimelines;

    public GameConfigData()
    {
        ListTimelines = new List<TimelineData>();
        for (int i = 0; i < 5; i++)
        {
            ListTimelines.Add(new TimelineData());
        }
    }

    public int GetExpToUpgrade(int timelineId, int eraId, int currentLevel)
    {
        var timeline = ListTimelines[timelineId];
        var era = timeline.ListEraData[eraId];
        return era.ExpToUpgradeCollector[currentLevel];
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
