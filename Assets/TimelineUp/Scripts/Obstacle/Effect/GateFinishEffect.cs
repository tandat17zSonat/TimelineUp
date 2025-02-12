using DarkTonic.PoolBoss;
using HyperCasualRunner.PopulatedEntity;

namespace TimelineUp.Obstacle
{
    public class GateFinishEffect : BaseObstacleEffect
    {
        public override void ApplyEffect(PopulatedEntity entity)
        {
            GameplayManager.Instance.SetResult(GameState.Win);
            PoolBoss.Despawn(transform);

            Destroy();
        }

        public override void ApplyEffect(Projectile projectile)
        {

        }

        public override void Reset()
        {
        }
    }

}