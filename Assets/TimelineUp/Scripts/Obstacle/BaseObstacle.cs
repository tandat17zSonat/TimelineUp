using System;
using System.Collections.Generic;
using HyperCasualRunner.PopulatedEntity;
using NaughtyAttributes;
using SonatFramework.UI;
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

        [SerializeField] SpriteRenderer mask;

        public int Id { get; set; }

        public bool CheckLocked()
        {
            if (_lockEffect == null) return false;
            else
            {
                return _lockEffect.Locked;
            }
        }
        public bool CheckMove()
        {
            if (_wayPointMover == null) return false;
            else
            {
                return _wayPointMover.IsMoving;
            }
        }

        public void SetProperty(int numProperty)
        {
            if (Type == ObstacleType.GateProjectileRange)
            {
                GetComponent<GateProjectileRangeEffect>().SetAmount(numProperty);

                var uiTools = PanelManager.Instance.GetPanel<UITools>();
                if (uiTools) uiTools.SetLog($"{Type} setProperty Ok");
            }
            else if (Type == ObstacleType.GateProjectileRate)
            {
                GetComponent<GateProjectileRateEffect>().SetAmount(numProperty);

                var uiTools = PanelManager.Instance.GetPanel<UITools>();
                if (uiTools) uiTools.SetLog($"{Type} setProperty Ok");
            }
        }

        public List<int> GetProperties()
        {
            var list = new List<int>();
            if (Type == ObstacleType.GateProjectileRange)
            {
                var gateProjectileRangeEffect = GetComponent<GateProjectileRangeEffect>();
                list.Add(gateProjectileRangeEffect.Amount);
            }
            else if (Type == ObstacleType.GateProjectileRate)
            {
                var gateProjectileRateEffect = GetComponent<GateProjectileRateEffect>();
                list.Add(gateProjectileRateEffect.Amount);
            }

            if (CheckLocked())
            {
                list.Add(_lockEffect.Amount);
            }
            return list;
        }

        public ObstacleType Type { get { return obstacleType; } }



        private void Awake()
        {
            if (_lockEffect) _lockEffect.Locked = false;
        }

        public void Initialize()
        {
            if (mask) mask.enabled = false;
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
                if (num <= 0)
                {
                    _lockEffect.Locked = false;
                }
                else
                {
                    _lockEffect.Locked = true;
                    _lockEffect.Initialize();
                    _lockEffect.SetAmount(num);
                }

                var uiTools = PanelManager.Instance.GetPanel<UITools>();
                if (uiTools) uiTools.SetLog($"{Type} setLock Ok");
            }
        }

        public void SetRun()
        {
            if (_wayPointMover) _wayPointMover.Initialize();
        }

        public void ResetRun()
        {
            if (_wayPointMover) _wayPointMover.Reset();
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

            foreach (var effect in _mainEffects)
            {
                effect.Reset();
            }
        }

        public void SetSelected(bool select)
        {
            mask.enabled = select;
        }

        public bool CheckSelected()
        {
            return mask.enabled;
        }
    }

}
