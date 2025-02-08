using System;
using DarkTonic.PoolBoss;
using DG.Tweening;
using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.ScriptableObjects;
using UnityEngine;

namespace HyperCasualRunner
{
    /// <summary>
    /// Projectile Pool spawns this, ProjectileShooterModifier uses this to shoot Projectile. It contains damage and speed of the projectiles. It damages Damageable types.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidbody;
        private ProjectileData projectileData;

        Tween _delayedCall;

        public int Damage { get { return projectileData.Damage; } }

        void OnTriggerEnter(Collider other)
        {
            //// Bắn chết quái vật
            //if (other.TryGetComponent(out Damageable damageable))
            //{
            //    damageable.TakeHit(_hitDamage);
            //    Release();
            //}

            if (other.TryGetComponent(out PopulationEffect populationEffect))
            {
                populationEffect.TakeHit(projectileData.Damage);
                Release();
            }
        }

        void OnDestroy()
        {
            _delayedCall.Kill();
        }

        public void Fire()
        {
            _rigidbody.velocity = transform.forward * projectileData.Speed;

            float existTime = projectileData.Range / projectileData.Speed;
            _delayedCall = DOVirtual.DelayedCall(existTime, Release, false);
        }

        void Release()
        {
            _delayedCall.Kill();
            PoolBoss.Despawn(transform);
        }

        public void SetInfo(ProjectileData data)
        {
            projectileData = data;
        }
    }
}
