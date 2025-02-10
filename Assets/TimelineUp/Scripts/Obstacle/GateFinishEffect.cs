using HyperCasualRunner;
using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulatedEntity;
using HyperCasualRunner.PopulationManagers;

public class GateFinishEffect : CollectableEffectBase
{
    public override void ApplyEffect(PopulatedEntity entity)
    {
        GameplayManager.Instance.SetResult(GameState.Win);
    }

    public override void ApplyHitEffect(Projectile projectile)
    {

    }
}