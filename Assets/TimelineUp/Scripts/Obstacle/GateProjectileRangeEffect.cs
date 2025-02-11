using DarkTonic.PoolBoss;
using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulatedEntity;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GateProjectileRangeEffect : CollectableEffectBase
{
    [SerializeField] int amount = -5;
    [SerializeField] MeshRenderer meshRender;
    [SerializeField] TMP_Text textAmount;

    [SerializeField] Material materialPositive;
    [SerializeField] Material materialNegative;

    public void Initialize()
    {
        Reset();
    }

    public override void ApplyEffect(PopulatedEntity entity)
    {
        GameplayManager.Instance.ProjectileRange += (float) amount / 10;
        PoolBoss.Despawn(transform);
    }

    public override void ApplyHitEffect(Projectile projectile)
    {
        amount += projectile.Damage;
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
        amount = -100;
        UpdateVisual();
    }
}