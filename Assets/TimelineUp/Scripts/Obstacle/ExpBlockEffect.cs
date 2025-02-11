using System;
using DG.Tweening;
using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulatedEntity;
using UnityEngine;

public class ExpBlockEffect : CollectableEffectBase
{
    private Sequence seqEffect;

    public void Initialize()
    {

    }

    public override void ApplyEffect(PopulatedEntity entity)
    {

    }

    public override void ApplyHitEffect(Projectile projectile)
    {
        var collectorLevel = GameplayManager.Instance.NumberInCollector;
        var exp = GameplayManager.Instance.ExpCollectorInGame;
        exp += projectile.Damage;

        var gameConfigData = GameManager.Instance.GameConfigData;
        if (collectorLevel + 1 < gameConfigData.GetNumberWarriorInCollector())
        {
            var expToUpgrade = gameConfigData.GetExpToUpgradeWarriorNumber(collectorLevel + 1);
            if (exp > expToUpgrade)
            {
                GameplayManager.Instance.NumberInCollector += 1; // tăng level collector
                exp -= expToUpgrade; 
            }
        }
        GameplayManager.Instance.ExpCollectorInGame = exp; // Cập nhật lại exp hiện tại

        EnableEffect();
    }

    private void EnableEffect()
    {
        if (seqEffect != null) seqEffect.Kill();

        seqEffect = DOTween.Sequence();
        seqEffect.Append(transform.DOScale(Vector3.one * 1.1f, 0.1f));
        seqEffect.Append(transform.DOScale(Vector3.one, 0.1f));
    }
}