using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterAddWarrior : BaseButtonBooster
{
    public override void HandleBooster()
    {
        playerData.BoosterLevel[id] += 1;

        playerData.NumberOfWarriors += 1;
    }
}
