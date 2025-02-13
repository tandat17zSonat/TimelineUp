using System.Collections.Generic;
using DarkTonic.PoolBoss;
using DG.Tweening;
using HyperCasualRunner.PopulatedEntity;
using UnityEngine;

public class ProjectileEnermy : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] SpriteRenderer _spriteRenderer;

    Tween _delayedCall;

    private int _damage;
    private float _speed;
    private float _range;

    public int Damage { get { return _damage; } }

    public void Initialize(int damage, float speed, float range)
    {
        _damage = damage;
        _speed = speed;
        _range = range;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PopulatedEntity entity))
        {
            var populationManager = GameplayManager.Instance.PopulationManager;
            populationManager.RemoveEntityFromCrowd(entity);

            if (populationManager.ListEntityInCrowd.Count == 0)
            {
                GameplayManager.Instance.SetResult(GameState.Loss);
            }
            Release();
        }
        else if(other.TryGetComponent(out Projectile projectileEntity))
        {
            Release();
        }
    }

    public void Fire()
    {
        _rigidbody.velocity = transform.forward * _speed;

        _delayedCall.Kill();
        float existTime = _range / Mathf.Abs(_speed);
        _delayedCall = DOVirtual.DelayedCall(existTime, Release, false);
    }

    public void Release()
    {
        _delayedCall.Kill();
        PoolBoss.Despawn(transform);
    }

    void SetVisual(int level)
    {
        ////if (_activeProjectile != null) _activeProjectile.SetActive(false);
        ////_activeProjectile = _renderersByLevel[level];
        ////_activeProjectile.SetActive(true);

        //_spriteRenderer.sprite = ListProjectileSprites[level];
    }
}