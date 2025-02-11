using DarkTonic.PoolBoss;
using HyperCasualRunner;
using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulatedEntity;

public class GateFinishEffect : CollectableEffectBase
{
    public override void ApplyEffect(PopulatedEntity entity)
    {
        GameplayManager.Instance.SetResult(GameState.Win);
        PoolBoss.Despawn(transform);
    }

    public override void ApplyHitEffect(Projectile projectile)
    {

    }
}