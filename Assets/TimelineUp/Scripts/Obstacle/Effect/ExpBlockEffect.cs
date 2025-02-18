using DarkTonic.PoolBoss;
using DG.Tweening;
using HyperCasualRunner.PopulatedEntity;
using UnityEngine;
using UnityEngine.UI;

namespace TimelineUp.Obstacle
{
    public class ExpBlockEffect : BaseObstacleEffect
    {
        private Sequence seqEffect;

        public override void ApplyEffect(PopulatedEntity entity)
        {
            Destroy();
        }

        public override void ApplyEffect(Projectile projectile)
        {
            var collectorLevel = GameplayManager.Instance.NumberInCollector;
            var exp = GameplayManager.Instance.ExpCollectorInGame;
            exp += projectile.Damage;

            var gameConfigData = DataManager.GameplayConfig;
            if (collectorLevel < gameConfigData.WarriorCollectorConfig.GetMaxWarriorNumber())
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

        public override void Reset()
        {
            seqEffect.Kill();
        }

        private void EnableEffect()
        {
            if (seqEffect != null) seqEffect.Kill();

            seqEffect = DOTween.Sequence();
            seqEffect.Append(transform.DOScale(Vector3.one * 1.1f, 0.1f));
            seqEffect.Append(transform.DOScale(Vector3.one, 0.1f));
        }
    }

}