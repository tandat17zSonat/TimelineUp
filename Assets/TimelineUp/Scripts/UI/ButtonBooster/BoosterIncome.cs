using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterIncome : BaseButtonBooster
{
    public override void HandleBooster()
    {
        playerData.BoosterLevel[id] += 1;
    }
}
