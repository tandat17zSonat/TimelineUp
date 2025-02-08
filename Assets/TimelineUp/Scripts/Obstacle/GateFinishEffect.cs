using HyperCasualRunner;
using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulationManagers;

public class GateFinishEffect : CollectableEffectBase
{
    public override void ApplyEffect(PopulationManagerBase manager)
    {
        GameplayManager.Instance.SetResult(GameState.Win);
    }

    public override void ApplyHitEffect(Projectile projectile)
    {

    }
}