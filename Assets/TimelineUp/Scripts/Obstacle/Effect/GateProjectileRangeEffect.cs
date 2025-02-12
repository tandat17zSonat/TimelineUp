using DarkTonic.PoolBoss;
using HyperCasualRunner.PopulatedEntity;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    public class GateProjectileRangeEffect : BaseObstacleEffect
    {
        [SerializeField] int amount = -5;
        [SerializeField] MeshRenderer meshRender;
        [SerializeField] TMP_Text textAmount;

        [SerializeField] Material materialPositive;
        [SerializeField] Material materialNegative;

        public override void ApplyEffect(PopulatedEntity entity)
        {
            GameplayManager.Instance.ProjectileRange += (float)amount / 10;
            
            Destroy();
        }

        public override void ApplyEffect(Projectile projectile)
        {
            amount += projectile.Damage;
            UpdateVisual();
        }


        private void UpdateVisual()
        {
            meshRender.material = amount >= 0 ? materialPositive : materialNegative;
            textAmount.text = amount >= 0 ? $"+ {Mathf.Abs(amount)}" : $"- {Mathf.Abs(amount)}";
        }

        public override void Reset()
        {
            amount = -100;
            UpdateVisual();
        }
    }

}