using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace HyperCasualRunner.CollectableEffects
{
    /// <summary>
    /// Use when you want regular collectables. Like gates, or collectable things on the ground.
    /// </summary>
    public class PopulationEffect : CollectableEffectBase
    {
        [SerializeField] int amount = -5;
        [SerializeField] MeshRenderer meshRender;
        [SerializeField] TMP_Text textAmount;

        [SerializeField] Material materialPositive;
        [SerializeField] Material materialNegative;

        public override void ApplyEffect(PopulatedEntity.PopulatedEntity entity)
        {
            amount = -100;
            Reset();
            //manager.AddPopulation(amount);
        }

        private void FixedUpdate()
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            meshRender.material = amount >= 0 ? materialPositive : materialNegative;
            textAmount.text = amount >= 0 ? $"+ {Mathf.Abs(amount)}" : $"- {Mathf.Abs(amount)}";
        }

        [Button("Setup", EButtonEnableMode.Editor)]
        void Reset()
        {
            UpdateVisual();
        }

        public void TakeHit(int number)
        {
            amount += number;
        }

        public override void ApplyHitEffect(Projectile projectile)
        {

        }
    }
}