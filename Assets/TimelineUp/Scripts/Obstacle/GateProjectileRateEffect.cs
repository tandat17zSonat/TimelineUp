using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulatedEntity;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GateProjectileRateEffect : CollectableEffectBase
{
    [SerializeField] int amount = -5;
    [SerializeField] MeshRenderer meshRender;
    [SerializeField] TMP_Text textAmount;

    [SerializeField] Material materialPositive;
    [SerializeField] Material materialNegative;

    public override void ApplyEffect(PopulatedEntity entity)
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