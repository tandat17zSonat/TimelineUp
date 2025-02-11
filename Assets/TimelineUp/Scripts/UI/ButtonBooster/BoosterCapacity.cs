using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterCapacity : BaseButtonBooster
{
    public override void HandleBooster()
    {
        var gameConfigData = GameManager.Instance.GameConfigData;
        playerData.ExpCollector += gameConfigData.ListBoosterCapacity[level];

        playerData.BoosterLevel[id] += 1;
    }
}
