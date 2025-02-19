using System;
using DarkTonic.PoolBoss;
using HyperCasualRunner.PopulatedEntity;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    public class GateProjectileRateEffect : BaseObstacleEffect
    {
        [SerializeField] int amount;
        //[SerializeField] MeshRenderer meshRender;
        [SerializeField] TMP_Text textAmount;
        [SerializeField] GameObject _layerRed;
        //[SerializeField] Material materialPositive;
        //[SerializeField] Material materialNegative;

        public override void ApplyEffect(PopulatedEntity entity)
        {
            if (amount > 0)
            {
                GameplayManager.Instance.ProjectileRate += (float)amount / 50;
            }
            Destroy();
        }


        public override void ApplyEffect(Projectile projectile)
        {
            amount += projectile.Damage;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            _layerRed.SetActive(amount <= 0);
            textAmount.text = amount >= 0 ? $"+ {Mathf.Abs(amount)}" : $"- {Mathf.Abs(amount)}";
        }

        [Button("Setup", EButtonEnableMode.Editor)]
        public override void Reset()
        {
            amount = -1;
            UpdateVisual();
        }

        public void SetAmount(int amount)
        {
            this.amount = amount;
            UpdateVisual();
        }
    }

}