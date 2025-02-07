using System;
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
        [SerializeField] int _hitDamage;
        [SerializeField] float _speed;
        [SerializeField] float _range;

        Tween _delayedCall;

        public int Damage { get { return _hitDamage; } }
        public ProjectilePool Pool { get; set; }

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
                populationEffect.TakeHit(_hitDamage);
                Release();
            }
        }

        void OnDestroy()
        {
            _delayedCall.Kill();
        }

        public void Fire()
        {
            Debug.Log($"transform.forward {transform.forward}");
            _rigidbody.velocity = transform.forward * _speed;

            float existTime = _range / _speed;
            _delayedCall = DOVirtual.DelayedCall(existTime, Release, false);
        }

        void Release()
        {
            _delayedCall.Kill();
            Pool.Release(this);
        }
    }
}
