public class BoosterCapacity : BaseButtonBooster
{
    public override void HandleBooster()
    {
        var gameConfigData = DataManager.GameplayConfig;
        var boosterCapacityConfig = gameConfigData.GetBoosterConfig(BoosterType.Capacity) as BoosterCapacityConfig;
        playerData.ExpCollector += boosterCapacityConfig.Exps[level];

        var exp = playerData.ExpCollector;
        var collectorLevel = playerData.NumberInCollector;

        if (collectorLevel < gameConfigData.WarriorCollectorConfig.GetMaxWarriorNumber())
        {
            var expToUpgrade = gameConfigData.GetExpToUpgradeWarriorNumber(collectorLevel + 1);
            if (exp > expToUpgrade)
            {
                collectorLevel += 1;
                exp -= expToUpgrade;
            }
        }


        playerData.ExpCollector = exp;
        playerData.NumberInCollector = collectorLevel;

        GameplayManager.Instance.NumberInCollector = collectorLevel;
        GameplayManager.Instance.ExpCollectorInGame = exp; // Cập nhật lại exp hiện tại

        playerData.BoosterLevel[id] += 1;
    }
}
