using HyperCasualRunner.PopulatedEntity;
using NaughtyAttributes;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    /// <summary>
    /// Quản lý các effect được thực hiện trong obstacle
    /// </summary>
    public class BaseObstacle : MonoBehaviour
    {
        [SerializeField] ObstacleType obstacleType;
        [InfoBox("This component centralizes collectableEffects by binding all the effects on the same gameObject")]
        [SerializeField] BaseObstacleEffect[] _mainEffects;
        [SerializeField] LockEffect _lockEffect;

        public ObstacleType Type { get { return obstacleType; } }

        public void Initialize(bool isLocked)
        {
            _lockEffect.Locked = isLocked;

            foreach(var effect in _mainEffects)
            {
                effect.Initialize();
            }
            _lockEffect.Initialize();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other)
            {
                // Nếu va chạm với entity
                if (other.TryGetComponent(out PopulatedEntity entity))
                {
                    ApplyCollectEffects(entity);
                }
                // Nếu va chạm với projectile
                else if (other.TryGetComponent(out Projectile projectile))
                {
                    ApplyCollectEffects(projectile);
                }
            }
        }

        void ApplyCollectEffects(PopulatedEntity entity)
        {
            if (_lockEffect.Locked == false)
            {
                foreach (BaseObstacleEffect collectableEffectBase in _mainEffects)
                {
                    collectableEffectBase.ApplyEffect(entity);
                }
            }
            else
            {
                _lockEffect.ApplyEffect(entity);
            }
        }

        void ApplyCollectEffects(Projectile projectile)
        {
            if (_lockEffect.Locked == false)
            {
                foreach (BaseObstacleEffect collectableEffectBase in _mainEffects)
                {
                    collectableEffectBase.ApplyEffect(projectile);
                }
            }
            else
            {
                _lockEffect.ApplyEffect(projectile);
            }
            projectile.Release();
        }
    }

}
