using HyperCasualRunner.PopulatedEntity;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    public abstract class BaseObstacleEffect : MonoBehaviour
    {
        protected bool IsCollider;

        public void Initialize()
        {
            IsCollider = false;
            Reset();
        }

        public abstract void ApplyEffect(PopulatedEntity entity);
        public abstract void ApplyEffect(Projectile projectile);

        public virtual void Destroy()
        {
            var obs = GetComponentInParent<BaseObstacle>();
            GameplayManager.Instance.ObstacleManager.Remove(obs);
        }

        public abstract void Reset();
    }
}
