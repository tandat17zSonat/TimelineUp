using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterWarriorUpgrade : BaseButtonBooster
{
    [SerializeField] GameObject[] _imageActives;
    public override void HandleBooster()
    {
        playerData.BoosterLevel[id] += 1;

        if(playerData.BoosterLevel[id] % 3 == 0)
        {
            playerData.LevelOfWarriors += 1;

            GameplayManager.Instance.UpdateLevelInCrowd();
        }
    }

    public override void UpdateVisual()
    {
        base.UpdateVisual();

        var numActives = playerData.BoosterLevel[id] % 3;
        for (int i = 0; i < _imageActives.Length; i++)
        {
            _imageActives[i].SetActive(i < numActives);
        }
    }

}