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
            GameplayManager.Instance.ExpCollectorInGame = exp;
        }

        [Button("Setup", EButtonEnableMode.Editor)]
        void Reset()
        {

        }

    }
}