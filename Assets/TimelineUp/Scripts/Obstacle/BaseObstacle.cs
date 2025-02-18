using System;
using System.Collections.Generic;
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
        [SerializeField] WayPointMover _wayPointMover;

        public ObstacleType Type { get { return obstacleType; } }

        private void Awake()
        {
            if (_lockEffect) _lockEffect.Locked = false;
        }

        public void Initialize()
        {
            foreach (var effect in _mainEffects)
            {
                effect.Initialize();
            }
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
            if (_lockEffect != null && _lockEffect.Locked == true)
            {
                _lockEffect.ApplyEffect(entity);
            }
            else
            {
                foreach (BaseObstacleEffect collectableEffectBase in _mainEffects)
                {
                    collectableEffectBase.ApplyEffect(entity);
                }
            }
        }

        void ApplyCollectEffects(Projectile projectile)
        {
            if (_lockEffect != null && _lockEffect.Locked == true)
            {
                _lockEffect.ApplyEffect(projectile);
            }
            else
            {
                foreach (BaseObstacleEffect collectableEffectBase in _mainEffects)
                {
                    collectableEffectBase.ApplyEffect(projectile);
                }
            }

            projectile.Release();
        }

        public void SetLock(int num)
        {
            if (_lockEffect)
            {
                _lockEffect.Locked = true;
                _lockEffect.Initialize();
                _lockEffect.SetHp(num);
            }
        }

        public void SetRun()
        {
            _wayPointMover.Initialize();
        }

        public virtual void SetProperties(List<int> properties)
        {
            if (Type == ObstacleType.GateProjectileRange)
            {
                GetComponent<GateProjectileRangeEffect>().SetAmount(properties[0]);
            }
            else if (Type == ObstacleType.GateProjectileRate)
            {
                GetComponent<GateProjectileRateEffect>().SetAmount(properties[0]);
            }
        }

        public void Reset()
        {
            if (_wayPointMover) _wayPointMover.Reset();

            foreach(var effect in _mainEffects)
            {
                effect.Reset();
            }
        }
    }

}
