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

    public override void Initialize(int level)
    {

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
}
