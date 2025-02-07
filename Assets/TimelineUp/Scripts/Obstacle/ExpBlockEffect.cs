using HyperCasualRunner.PopulationManagers;
using NaughtyAttributes;
using TimelineUp.Obstacle;
using TimelineUp.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace HyperCasualRunner.CollectableEffects
{
    public class ExpBlockEffect : CollectableEffectBase
    {

        public override void ApplyEffect(PopulationManagerBase manager)
        {

        }

        public void ApplyHitEffect(Projectile projectile)
        {
            Debug.Log("Hit");

            var collectorLevel = GameplayManager.Instance.CollectorLevel;
            var exp = GameplayManager.Instance.ExpCollectorInGame;
            exp += projectile.Damage;

            var gameConfigData = GameManager.Instance.GameConfigData;
            var expToUpgrade = gameConfigData.GetExpToUpgrade(0, 0, collectorLevel + 1);

            if ( exp > expToUpgrade)
            {
                GameplayManager.Instance.CollectorLevel += 1;
                exp -= expToUpgrade;
            }

            GameplayManager.Instance.ExpCollectorInGame = exp;
        }

        [Button("Setup", EButtonEnableMode.Editor)]
        void Reset()
        {

        }

    }
}