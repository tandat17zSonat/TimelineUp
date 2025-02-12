using DarkTonic.PoolBoss;
using HyperCasualRunner.PopulatedEntity;
using UnityEngine;

public class ProjectileShooterModifiable : BaseModifiable
{
    [Header("")]
    [SerializeField] PopulatedEntity _entity;

    [Header("")]
    [SerializeField] Transform _projectilePrefab;
    [SerializeField] Transform _container;

    [SerializeField] Transform _spawnPoint;

    private bool _fire = false;
    private float _shootInterval;
    private float _timer = 0f;

    public override void Initialize(int level)
    {
        _fire = false;
    }

    public void Play()
    {
        _timer = Random.value; // bắn ngẫu nhiên ( khong đều)
        _fire = true;
    }

    public void Shoot()
    {
        var spawned = PoolBoss.Spawn(_projectilePrefab, Vector3.zero, Quaternion.identity, null);
        //spawned.transform.SetPositionAndRotation(_spawnPoint.position, transform.rotation); 

        spawned.transform.position = _spawnPoint.position;
        Projectile projectile = spawned.GetComponent<Projectile>();
        projectile.Initialize(_entity.Level);
        projectile.Fire();
    }

    private void Update()
    {
        if (_fire)
        {
            _timer += Time.deltaTime;

            _shootInterval = 1.0f / GameplayManager.Instance.ProjectileRate;
            if (_timer >= _shootInterval)
            {
                _timer -= _shootInterval;
                Shoot();
            }
        }
    }

    public void Disappear()
    {
        _fire = false;
    }
}
