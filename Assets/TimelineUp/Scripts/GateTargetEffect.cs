using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulationManagers;

public class GateTargetEffect : CollectableEffectBase
{
    public override void ApplyEffect(PopulationManagerBase manager)
    {
        GameplayManager.Instance.SetResult(GameState.Win);
    }
}