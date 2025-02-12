public class BoosterCapacity : BaseButtonBooster
{
    public override void HandleBooster()
    {
        var gameConfigData = GameManager.Instance.GameConfigData;
        playerData.ExpCollector += gameConfigData.ListBoosterCapacity[level];

        var exp = playerData.ExpCollector;
        var collectorLevel = GameplayManager.Instance.NumberInCollector;

        if (collectorLevel < gameConfigData.ListWarriorCollectorDatas.NumberMaxWarrior)
        {
            var expToUpgrade = gameConfigData.GetExpToUpgradeWarriorNumber(collectorLevel + 1);
            if (exp > expToUpgrade)
            {
                collectorLevel += 1;
                exp -= expToUpgrade;
            }
        }

        playerData.NumberOfWarriors = collectorLevel;
        playerData.ExpCollector = exp;

        GameplayManager.Instance.NumberInCollector = collectorLevel; // tăng level collector
        GameplayManager.Instance.ExpCollectorInGame = exp; // Cập nhật lại exp hiện tại

        playerData.BoosterLevel[id] += 1;
    }

}
