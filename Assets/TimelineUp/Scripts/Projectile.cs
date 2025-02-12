using DarkTonic.PoolBoss;
using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] GameObject[] _renderersByLevel;
    Tween _delayedCall;

    private int _damage;
    private float _speed;
    private float _range;
    private GameObject _activeProjectile;
    public int Damage { get { return _damage; } }

    public void Initialize(int level)
    {
        var gameConfigData = GameManager.Instance.GameConfigData;

        _damage = gameConfigData.ListWarriorDatas[level].Damage;
        _speed = GameplayManager.Instance.ProjectileSpeed;
        _range = GameplayManager.Instance.ProjectileRange;

        SetVisual(level);
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent(out PopulationEffect populationEffect))
        //{
        //    populationEffect.TakeHit(projectileData.Damage);
        //    Release();
        //}
    }

    public void Fire()
    {
        _rigidbody.velocity = transform.forward * _speed;

        _delayedCall.Kill();
        float existTime = _range / _speed;
        _delayedCall = DOVirtual.DelayedCall(existTime, Release, false);
    }

    public void Release()
    {
        PoolBoss.Despawn(transform);
    }

    void SetVisual(int level)
    {
        if (_activeProjectile != null) _activeProjectile.SetActive(false);
        _activeProjectile = _renderersByLevel[level];
        _activeProjectile.SetActive(true);
    }
}