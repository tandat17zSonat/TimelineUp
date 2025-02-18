using TimelineUp.Data;
using UnityEngine;

public class BoosterWarriorUpgrade : BaseButtonBooster
{
    [SerializeField] GameObject[] _imageActives;
    public override void HandleBooster()
    {

        playerData.BoosterLevel[id] += 1;

        if (playerData.BoosterLevel[id] % 3 == 0)
        {
            playerData.LevelOfWarriors += 1;

            var _populationManager = GameplayManager.Instance.PopulationManager;
            foreach (var entity in _populationManager.ListEntityInCrowd)
            {
                entity.SetInfo(playerData.LevelOfWarriors);
            }

        }
    }

    public override void UpdateVisual()
    {
        base.UpdateVisual();

        _textLevel.text = $"Level {playerData.LevelOfWarriors + 1}";

        var numActives = playerData.BoosterLevel[id] % 3;
        for (int i = 0; i < _imageActives.Length; i++)
        {
            _imageActives[i].SetActive(i < numActives);
        }
    }

}