using System;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace HyperCasualRunner.CollectableEffects
{
    public class ExpBlockEffect : CollectableEffectBase
    {

        private Sequence seqEffect;
        public override void ApplyEffect(PopulatedEntity.PopulatedEntity entity)
        {

        }

        public override void ApplyHitEffect(Projectile projectile)
        {
            var collectorLevel = GameplayManager.Instance.NumberInCollector;
            var exp = GameplayManager.Instance.ExpCollectorInGame;
            exp += projectile.Damage;

            var gameConfigData = GameManager.Instance.GameConfigData;
            if(collectorLevel + 1 < gameConfigData.GetNumberWarriorInCollector())
            {
                var expToUpgrade = gameConfigData.GetExpToUpgradeWarriorNumber(collectorLevel + 1);
                if (exp > expToUpgrade)
                {
                    GameplayManager.Instance.NumberInCollector += 1;
                    exp -= expToUpgrade;
                }
            }

            EnableEffect();
            GameplayManager.Instance.ExpCollectorInGame = exp;
        }

        private void EnableEffect()
        {
            if(seqEffect != null) seqEffect.Kill();

            seqEffect = DOTween.Sequence();
            seqEffect.Append(transform.DOScale(Vector3.one * 1.1f, 0.1f));
            seqEffect.Append(transform.DOScale(Vector3.one, 0.1f));
        }

        [Button("Setup", EButtonEnableMode.Editor)]
        void Reset()
        {

        }

    }
}