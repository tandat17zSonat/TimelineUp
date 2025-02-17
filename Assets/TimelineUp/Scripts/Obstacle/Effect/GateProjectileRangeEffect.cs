using HyperCasualRunner.PopulatedEntity;
using TMPro;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    public class GateProjectileRangeEffect : BaseObstacleEffect
    {
        [SerializeField] int amount = -5;
        //[SerializeField] MeshRenderer meshRender;
        [SerializeField] TMP_Text textAmount;
        [SerializeField] GameObject _layerRed;

        //[SerializeField] Material materialPositive;
        //[SerializeField] Material materialNegative;

        public override void ApplyEffect(PopulatedEntity entity)
        {
            if (amount > 0)
            {
                GameplayManager.Instance.ProjectileRange += (float)amount / 100;
            }

            Destroy();
        }

        public override void ApplyEffect(Projectile projectile)
        {
            amount += projectile.Damage;
            Debug.Log($"{amount}");
            UpdateVisual();
        }


        private void UpdateVisual()
        {
            //meshRender.material = amount >= 0 ? materialPositive : materialNegative;
            _layerRed.SetActive(amount <= 0);
            textAmount.text = Utils.FormatNumber(amount);
        }

        public override void Reset()
        {
            amount = -2;
            UpdateVisual();
        }

        public void SetAmount(int amount)
        {
            this.amount = amount;
            UpdateVisual();
        }
    }

}